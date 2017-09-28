using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Scoring
{       
    public class ScorerClass
    {
        private const int MAX_PINS = 10;
        private const int MAX_FRAMES = 10;
        private const int FIRST_FRAME = 1;

        private class FrameClass
        {
            private int _firstBallValue;
            private int _secondBallValue;
            private int _thirdBallValue;            

            private bool areAllPinsDown(int pinsDown)
            {
                return (pinsDown == MAX_PINS);
            }

            public bool FirstBallThrown { get; private set; }
            public bool SecondBallThrown { get; private set; }
            public bool ThirdBallThrown { get; private set; }

            public int FirstBallValue => _firstBallValue;
            public int SecondBallValue => _secondBallValue;

            public void SetFirstBallValue(int pinsKnockedDown)
            {
                FirstBallThrown = true;
                _firstBallValue = pinsKnockedDown;
                IsStrike = areAllPinsDown(pinsKnockedDown);
                IsSpare = false;

            }
            public void SetSecondBallValue(int pinsKnockedDown)
            {
                SecondBallThrown = true;
                _secondBallValue = pinsKnockedDown;
                if (Number < MAX_FRAMES)
                {
                    IsSpare = areAllPinsDown(_firstBallValue + pinsKnockedDown);
                    IsStrike = false;
                }
                else
                {
                    IsStrike = areAllPinsDown(pinsKnockedDown);
                    if (!IsStrike)
                    {
                        IsSpare = areAllPinsDown(_firstBallValue + pinsKnockedDown);
                    }
                }
            }

            public void SetThirdBallValue(int pinsKnockedDown)
            {
                ThirdBallThrown = true;
                _thirdBallValue = pinsKnockedDown;
                IsSpare = areAllPinsDown(_secondBallValue + pinsKnockedDown);
            }

            public bool IsStrike;
            public bool IsSpare;

            public int Number { get; set; }                      

            public int Total
            {
                get
                {
                    if (Number == MAX_FRAMES)
                    {
                        return _firstBallValue + _secondBallValue + _thirdBallValue;                        
                    }
                    return _firstBallValue + _secondBallValue;
                }
            }

            public string Score
            {
                get
                {
                    string sResult = "";
                    if (FirstBallThrown)
                    {
                        if (areAllPinsDown(_firstBallValue)) sResult = "X";
                        else sResult = Convert.ToString(_firstBallValue);
                    }
                    if (SecondBallThrown)
                    {
                        if (areAllPinsDown(_secondBallValue)) sResult += " X";
                        else if (areAllPinsDown(_firstBallValue + _secondBallValue)) sResult += " /";
                        else sResult += " " + Convert.ToString(_secondBallValue);
                    }
                    if (ThirdBallThrown)
                    {
                        if (IsStrike) sResult += " X";
                        else if(IsSpare) sResult += " /";
                        else sResult += " " + Convert.ToString(_thirdBallValue);
                    }

                    return(sResult);
                }                 
            }            
        }
        private readonly FrameClass[] Frames;

        private bool _isTurkey(int frameNumber)
        {
            // frameNumber is indexed at 1 so we have to adjust for that
            // find the total number of consecutive strikes
            // and then make sure it's evently divisable by 3
            var iFrameIndex = frameNumber - 1;            
            var i = iFrameIndex;
            var iStrikes = 0;
            var bConsecutive = true;
            while (bConsecutive && i>=0)
            {
                if (Frames[i].IsStrike) iStrikes++;
                else bConsecutive = false;
                i--;
            }
            return (iStrikes >= 3 && iStrikes % 3 == 0);
        }

        private void IncrementFrame()
        {
            if (Frame < MAX_FRAMES) Frame++;
        }
        
        private int getFrameIndex(int frame)
        {
            return frame - 1;
        }        

        public ScorerClass()
        {            
            Frames = new FrameClass[MAX_FRAMES];
            for (int i = 0; i < MAX_FRAMES; i++)
            {
                Frames[i] = new FrameClass();
            }

            Frame = 1;
        }

        public int Frame { get; set; }

        public string FrameScore
        {
            get
            {
                // If we're at the 1st frame (index 0) then just show base index -1
                // If we're at the last frame (index LAST_FRAME) then just show base index -1
                // Otherwise we have to subtract 2 Why?
                //  Subtract 1 becuase Frame is index base 1 not zero
                //  Subtract 1 becuase we need to show the score for the frame
                //             that was just bowled, not the current one
                //             since the frame counter is automatically increment
                //             after bowling # of balls in the frame
                if (Frame == FIRST_FRAME) return Frames[Frame-1].Score;
                if (Frame == MAX_FRAMES) return Frames[Frame-1].Score;
                return Frames[Frame - 2].Score;
            }
        }

        public int Total()
        {
            var iResult = 0;
            for (var i = 0; i < MAX_FRAMES; i++)
            {
                iResult += Frames[i].Total;

                if (i < MAX_FRAMES)
                {
                    if (Frames[i].IsStrike)
                    {                            
                        for (int iNext = i+1; iNext <= i + 2; iNext++)
                        {
                            if (iNext < MAX_FRAMES)
                            {
                                // If we're 2 frames away 
                                // only add the first ball
                                if (iNext == i + 2)
                                    iResult += Frames[iNext].FirstBallValue;
                                else iResult += Frames[iNext].FirstBallValue + Frames[iNext].SecondBallValue;
                            }
                        }
                    }
                    else if (Frames[i].IsSpare)
                    {
                        if (i + 1 < MAX_FRAMES)
                        {
                            iResult += Frames[i + 1].FirstBallValue;
                        }
                    }
                }
            }
            return (iResult);
        }

        public void bowlBall(int pinsDown)
        {
            var frame = Frames[getFrameIndex(Frame)];
            frame.Number = Frame;
                        
            if (!frame.FirstBallThrown)
            {
                frame.SetFirstBallValue(pinsDown);
                if(frame.IsStrike) IncrementFrame();
            }
            else if (!frame.SecondBallThrown)
            {
                frame.SetSecondBallValue(pinsDown);
                IncrementFrame();
            }
            else if (!frame.ThirdBallThrown)
            {
                if (frame.Number == MAX_FRAMES)
                {
                    frame.SetThirdBallValue(pinsDown);
                }
            }

            if (_isTurkey(frame.Number)) Message = "Turkey!";
            else Message = "";
        }

        public string Message { get; set; }
    }
}
