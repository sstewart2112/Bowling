Feature: Scoring
	I want to show the score of a bowling game
	after bowling 10 frames

@mytag
Scenario: Bowling a strike
Given I am on the first frame
When I bowl a strike 
Then the frame score should show "X"

Scenario: Bowling a spare
Given I am on the first frame
When I bowl a spare 
Then the frame score should show "/"

Scenario: Bowling a frame not knocking all of the pins down
Given I am on the first frame
When I knock down 3 pins on the first ball
And I knock down 4 pins on the second ball
Then the frame score should show "7"
And the total score should be "7"

Scenario: Bowling a perfect game
Given I am on the first frame
When I bowl a perfect game
Then the frame score should show "X X X"
And the total score should be "300"

Scenario: 10th frame spare
Given I am on the first frame
When I bowl 10 strikes in a row
And I knock down 5 pins on the second ball
And I knock down 5 pins on the third ball
Then the total should be 285
And the frame score should show "X 5 /"

Scenario: First Frame
Given I am on the first frame
Then the frame score should show ""

Scenario: Five frames with a strike in the third frame
Given I am on the first frame
When I knock down 3 pins on the first ball
And I knock down 4 pins on the second ball
And I knock down 0 pins on the first ball 
And I knock down 8 pins on the second ball
And I bowl 1 strikes in a row
And I knock down 8 pins on the first ball
And I knock down 1 pins on the second ball 
Then the frame score should show "9"
And the total should be 43

Scenario: Nine strikes in a row with no marks in the tenth frame
Given I am on the first frame
When I bowl 9 strikes in a row
And I knock down 4 pins on the first ball
And I knock down 5 pins on the second ball
Then the frame score should show "4 5"
And the total should be 262

Scenario: Three strikes in a row with no mark in fourth frame
Given I am on the first frame
When I bowl 3 strikes in a row
And I knock down 4 pins on the first ball
And I knock down 1 pins on the second ball
Then the frame score should show "5"
And the total should be 74
