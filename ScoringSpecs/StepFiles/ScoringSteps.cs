using Scoring;
using TechTalk.SpecFlow;
using FluentAssertions;
using Microsoft.VisualStudio.QualityTools.UnitTestFramework;

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
        }

        
        [Then(@"the frame score should show ""(.*)""")]
        public void ThenTheFrameScoreShouldShow(string frameScore)
        {
            _scorer.FrameScore.Should().Be(frameScore);
        }

        [Then(@"the total score should be (.*)")]
        public void ThenTheTotalScoreShouldBe(int total)
        {
            _scorer.Total().Should().Be(total);
        }
                
        [Given(@"I bowl (.*) strikes in a row")]
        [When(@"I bowl (.*) strikes in a row")]
        public void WhenIBowlStrikesInARow(int strikes)
        {
            for (var i = 1; i <= strikes; i++)
            {
                _scorer.bowlBall(10);
            }
        }
        
        [Given(@"I bowl a ball knocking down (.*) pins")]
        [When(@"I bowl a ball knocking down (.*) pins")]
        public void WhenIBowlABallKnockingDownPins(int pinsDown)
        {
            _scorer.bowlBall(pinsDown);
        }

                
        [Then(@"I should be on frame number (.*)")]
        public void ThenIShouldBeOnFrameNumber(int frameNumber)
        {
            _scorer.Frame.Should().Be(frameNumber);
        }
       
        [Given(@"A Message shows ""(.*)""")]
        [Then(@"A Message shows ""(.*)""")]
        public void GivenAMessageShows(string message)
        {
            _scorer.Message.Should().Be(message);
        }
        
    }
}
