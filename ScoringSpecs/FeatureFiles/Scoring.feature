Feature: Scoring
	I want to show the score of a bowling game
	after bowling 10 frames

@mytag
Scenario: Bowling a strike
Given I am on the first frame
When I bowl a strike 
Then the frame score should show "X"
	And I should be on frame number 2

Scenario: Bowling a spare
Given I am on the first frame
And I bowl a ball knocking down 5 pins
When I bowl a ball knocking down 5 pins
Then the frame score should show "5 /"
	And I should be on frame number 2

Scenario: Bowling a frame not knocking all of the pins down
Given I am on the first frame
	And I bowl a ball knocking down 3 pins
When I bowl a ball knocking down 4 pins
Then the frame score should show "3 4"
	And the total score should be "7"
	And I should be on frame number 2

Scenario: Bowling a perfect game
Given I am on the first frame
When I bowl 12 strikes in a row
Then the frame score should show "X X X"
	And the total score should be "300"
	And I should be on frame number 10

Scenario: 10th frame spare
Given I am on the first frame
	And I bowl 10 strikes in a row
	And I bowl a ball knocking down 5 pins
When I bowl a ball knocking down 5 pins
Then the total should be 285
	And the frame score should show "X 5 /"
	And I should be on frame number 10

Scenario: First Frame
Given I am on the first frame
Then the frame score should show ""
	And I should be on frame number 1

Scenario: Four frames with a strike in the third frame
Given I am on the first frame
	And I bowl a ball knocking down 3 pins
	And I bowl a ball knocking down 4 pins
	And I bowl a ball knocking down 0 pins 
	And I bowl a ball knocking down 8 pins
	And I bowl 1 strikes in a row
	And I bowl a ball knocking down 8 pins
When I bowl a ball knocking down 1 pins
Then the frame score should show "8 1"
	And the total should be 43
	And I should be on frame number 5

Scenario: Nine strikes in a row with no marks in the tenth frame
Given I am on the first frame
	And I bowl 9 strikes in a row
	And I bowl a ball knocking down 4 pins
When I bowl a ball knocking down 5 pins
Then the frame score should show "4 5"
	And the total should be 262
	And I should be on frame number 10

Scenario: Three strikes in a row with no mark in fourth frame
Given I am on the first frame
	And I bowl 3 strikes in a row
	And A Message shows "Turkey!"
	And I bowl a ball knocking down 4 pins
When I bowl a ball knocking down 1 pins
Then the frame score should show "4 1"
	And the total should be 74
	And I should be on frame number 5

Scenario: First three frames with a spare on the second frame
Given I am on the first frame
	And I bowl a ball knocking down 2 pins
	And I bowl a ball knocking down 6 pins
	And I bowl a ball knocking down 5 pins
	And I bowl a ball knocking down 5 pins
	And I bowl a ball knocking down 0 pins
When I bowl a ball knocking down 3 pins
Then the total should be 21
	And the frame score should show "0 3"
	And I should be on frame number 4

Scenario: Bowl a game with no mark for third ball in tenth frame
Given I am on the first frame
	And I bowl 9 strikes in a row
	And I bowl a ball knocking down 3 pins
	And I bowl a ball knocking down 7 pins
When I bowl a ball knocking down 4 pins
Then the total should be 267
	And the frame score should show "3 / 4" 

Scenario: Bowl four strikes in a row and show turkey message on the third one only
Given I am on the first frame
	And I bowl 3 strikes in a row
	And A Message shows "Turkey!"
When I bowl 1 strikes in a row
Then A Message shows "" 


