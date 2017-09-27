using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scoring;
using TechTalk.SpecFlow;

namespace ScoringSpecs.StepFiles
{
    [Binding]
    public class ScoringSteps
    {
        private ScorerClass _scorer;

        [Given(@"I am on the first frame")]
        public void GivenIAmOnTheFirstFrame()
        {
            _scorer = new ScorerClass();
            Assert.AreEqual(1, _scorer.Frame);
        }

        [When(@"I bowl a strike")]
        public void WhenIBowlAStrike()
        {
            _scorer.bowlFirstBall(10);            
        }

        [Then(@"the frame score should show ""(.*)""")]
        public void ThenTheFrameScoreShouldShow(string frameScore)
        {
            Assert.AreEqual(frameScore, _scorer.FrameScore);
        }

        [When(@"I bowl a spare")]
        public void WhenIBowlASpare()
        {            
            _scorer.bowlFirstBall(5);
            _scorer.bowlSecondBall(5);
        }
        

        [When(@"I knock down (.*) pins on the first ball")]
        public void WhenIKnockDownPinsOnTheFirstBall(int pinsKnockedDown)
        {
            _scorer.bowlFirstBall(pinsKnockedDown);
        }

        [When(@"I knock down (.*) pins on the second ball")]
        public void WhenIKnockDownPinsOnTheSecondBall(int pinsKnockedDown)
        {
            _scorer.bowlSecondBall(pinsKnockedDown);
        }


        [Then(@"the total score should be ""(.*)""")]
        public void ThenTheTotalScoreShouldBe(int total)
        {
            Assert.AreEqual(total, _scorer.Total());
        }

        [When(@"I bowl a perfect game")]
        public void WhenIBowlAPerfectGame()
        {
            // A perfect game consists of 12 strikes in a row
            // You have to bowl 10 strikes in a row to get to the 10th frame
            // When you get to the 10th frame if you get a strike or a spare
            // you get another ball.  So if you have to bowl 2 more strikes to
            // have a perfect game
            _scorer = new ScorerClass();
            for (var iFrame = 1; iFrame <= 10; iFrame++)
            {
                _scorer.bowlFirstBall(10);
            }
            // Now the 2 extra balls that are strikes
            _scorer.bowlSecondBall(10);
            _scorer.bowlThirdBall(10);           
        }
        
        [Then(@"the total should be (.*)")]
        public void ThenTheTotalShouldBe(int score)
        {
            Assert.AreEqual(score,_scorer.Total());
        }

        [When(@"I knock down (.*) pins on the third ball")]
        public void WhenIKnockDownPinsOnTheThirdBall(int pinsKnockedDown)
        {
            _scorer.bowlThirdBall(pinsKnockedDown);
        }

        [When(@"I bowl (.*) strikes in a row")]
        public void WhenIBowlStrikesInARow(int strikes)
        {
            for (var i = 1; i <= strikes; i++)
            {
                _scorer.bowlFirstBall(10);
            }
        }
    }
}
