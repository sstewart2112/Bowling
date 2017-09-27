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
        private const int MAX_FRAMES = 10;
        private const int FIRST_FRAME = 1;
        private class FrameClass
        {
            private int _firstBallValue;
            private int _secondBallValue;
            private int _thirdBallValue;
            private bool _firstBallThrown;
            private bool _secondBallThrown;
            private bool _thirdBallThrown;
            private readonly bool _lastFrame;           

            private bool areAllPinsDown(int pinsDown)
            {
                return (pinsDown == 10);
            }

            public FrameClass(bool lastFrame)
            {
                _lastFrame = lastFrame;
            }
            
            public int FirstBallValue => _firstBallValue;
            public int SecondBallValue => _secondBallValue;

            public void SetFirstBallValue(int pinsKnockedDown)
            {
                _firstBallThrown = true;
                _firstBallValue = pinsKnockedDown;
                IsStrike = areAllPinsDown(pinsKnockedDown);
                IsSpare = false;

            }
            public void SetSecondBallValue(int pinsKnockedDown)
            {
                _secondBallThrown = true;
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
                _thirdBallThrown = true;
                _thirdBallValue = pinsKnockedDown;
                IsSpare = areAllPinsDown(_secondBallValue + pinsKnockedDown);
            }

            public bool IsStrike;
            public bool IsSpare;

            public int Number { private get; set; }                      

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
                    if (_lastFrame)
                    {
                        // The 10th frame can show up to 3 scores based on the number of balls thrown                        
                        string sResult = "";
                        if (_firstBallThrown)
                        {
                            if (areAllPinsDown(_firstBallValue)) sResult = "X";
                            else sResult = Convert.ToString(_firstBallValue);
                        }
                        if (_secondBallThrown)
                        {
                            if (areAllPinsDown(_secondBallValue)) sResult += " X";
                            else if (areAllPinsDown(_firstBallValue + _secondBallValue)) sResult += " /";
                            else sResult += " " + Convert.ToString(_secondBallValue);
                        }
                        if (_thirdBallThrown)
                        {
                            if (IsStrike) sResult += " X";
                            else if(IsSpare) sResult += " /";
                            else sResult += " " + Convert.ToString(_thirdBallValue);
                        }

                        return(sResult);
                    }
                    else
                    {
                        if (IsStrike) return ("X");
                        if (IsSpare) return ("/");
                        if (!_firstBallThrown) return ("");
                        return Convert.ToString(Total);
                    }

                    
                    
                }                 
            }            
        }
        private readonly FrameClass[] Frames;

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
                // i is indexed at zero 
                // MAX_FRAMES is #of frames indexed at 1
                Frames[i] = new FrameClass(i+1 == MAX_FRAMES);
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

        public void bowlFirstBall(int pinsDown)
        {
            var frame = Frames[getFrameIndex(Frame)];
            frame.Number = Frame;
            frame.SetFirstBallValue(pinsDown); 
            if(frame.IsStrike) IncrementFrame();
        }

        public void bowlSecondBall(int pinsDown)
        {
            var frame = Frames[getFrameIndex(Frame)];
            frame.Number = Frame;
            frame.SetSecondBallValue(pinsDown);
            IncrementFrame();
        }

        public void bowlThirdBall(int pinsDown)
        {
            var frame = Frames[getFrameIndex(Frame)];
            frame.Number = Frame;
            frame.SetThirdBallValue(pinsDown);
        }
    }
}
