using Scoring;
using TechTalk.SpecFlow;
using FluentAssertions;

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

        [When(@"I bowl a strike")]
        public void WhenIBowlAStrike()
        {
            _scorer.bowlBall(10);            
        }

        [Then(@"the frame score should show ""(.*)""")]
        public void ThenTheFrameScoreShouldShow(string frameScore)
        {
            _scorer.FrameScore.Should().Be(frameScore);
        }                
        
        [Then(@"the total score should be ""(.*)""")]
        public void ThenTheTotalScoreShouldBe(int total)
        {
            _scorer.Total().Should().Be(total);
        }
            
        [Then(@"the total should be (.*)")]
        public void ThenTheTotalShouldBe(int score)
        {
            _scorer.Total().Should().Be(score);
        }        

        [When(@"I bowl (.*) strikes in a row")]
        public void WhenIBowlStrikesInARow(int strikes)
        {
            for (var i = 1; i <= strikes; i++)
            {
                _scorer.bowlBall(10);
            }
        }

        [Given(@"I bowl (.*) strikes in a row")]
        public void GivenIBowlStrikesInARow(int strikes)
        {
            for (var i = 1; i <= strikes; i++)
            {
                _scorer.bowlBall(10);
            }
        }


        [When(@"I bowl a ball knocking down (.*) pins")]
        public void WhenIBowlABallKnockingDownPins(int pinsDown)
        {
            _scorer.bowlBall(pinsDown);
        }

        [Given(@"I bowl a ball knocking down (.*) pins")]
        public void GivenIBowlABallKnockingDownPins(int pinsDown)
        {
            _scorer.bowlBall(pinsDown);
        }


        [Then(@"I should be on frame number (.*)")]
        public void ThenIShouldBeOnFrameNumber(int frameNumber)
        {
            _scorer.Frame.Should().Be(frameNumber);
        }

        [When(@"A Message shows ""(.*)""")]
        public void WhenAMessageShows(string message)
        {
            _scorer.Message.Should().Be(message);
        }

        [Given(@"A Message shows ""(.*)""")]
        public void GivenAMessageShows(string message)
        {
            _scorer.Message.Should().Be(message);
        }


        [Then(@"A Message shows ""(.*)""")]
        public void ThenAMessageShows(string message)
        {
            _scorer.Message.Should().Be(message);
        }
    }
}
