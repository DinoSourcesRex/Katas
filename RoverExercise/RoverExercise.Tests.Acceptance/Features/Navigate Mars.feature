Feature: Navigate Mars
	In order to successfully navigate Mars
	As a driver
	I want to be able to send commands to the Rover

Scenario: Will the Rover successfully accept 5 commands
	Given Mars has an area of x:100 by y:100 split into 100 sectors
		And The Rover starts at position x:1 y:1 facing South
		And The following commands
			| Direction | Distance |
			| Forward   | 50       |
			| Left      | 0        |
			| Forward   | 23       |
			| Left      | 0        |
			| Forward   | 4        |
	When I execute the commands
	Then the rover should arrive at sector 4624 with coordinates x:24 y:47 facing North

Scenario: Will the Rover successfully ignore excess commands
	Given Mars has an area of x:100 by y:100 split into 100 sectors
		And The Rover starts at position x:1 y:1 facing South
		And The following commands
			| Direction | Distance |
			| Forward   | 50       |
			| Left      | 0        |
			| Forward   | 23       |
			| Left      | 0        |
			| Forward   | 4        |
			| Forward   | 4        |
			| Forward   | 4        |
			| Forward   | 4        |
	When I execute the commands
	Then the rover should arrive at sector 4624 with coordinates x:24 y:47 facing North

Scenario: Will the Rover successfully ignore unsafe commands
	Given Mars has an area of x:100 by y:100 split into 100 sectors
		And The Rover starts at position x:1 y:1 facing South
		And The following commands
			| Direction | Distance |
			| Forward   | 46       |
			| Left      | 0        |
			| Forward   | 23       |
			| Forward   | 100      |
	When I execute the commands
	Then the rover should arrive at sector 4624 with coordinates x:24 y:47 facing East