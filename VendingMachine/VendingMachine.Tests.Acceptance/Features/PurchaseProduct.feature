Feature: PurchaseProduct
	In order to sate my need to eat and drink
	As a customer
	I want to be able to purchase a product

Scenario: Purchasing an item with the exact change
	Given that a Snickers costs a total of £1.5
	When I enter £0.5 into the machine
	And I enter £1 into the machine
	And I select Snickers
	Then the vending machine should give me my Snickers
	And there should be £0 change in the coin tray
	And the vending machine should display THANK YOU

Scenario: Purchasing an item then checking the display again
	Given that a Cucumber costs a total of £1
	When I enter £1 into the machine
	And I select Cucumber
	Then the vending machine should give me my Cucumber
	And the vending machine should display THANK YOU
	Then I wait 1000 milliseconds
	And the vending machine should display INSERT COIN

Scenario: Purchasing an item with inexact change
	Given that a Cola costs a total of £2
	When I enter £2 into the machine
	And I enter £0.50 into the machine
	And I select Cola
	Then the vending machine should give me my Cola
	And there should be £0.50 change in the coin tray
	And the vending machine should display THANK YOU

Scenario: Purchasing an item and not entering enough money
	Given that a Apple costs a total of £5
	When I enter £0.25 into the machine
	And I select Apple
	Then the vending machine should display INSERT £4.75

Scenario: Selecting an item first
	Given that a Gherkin costs a total of £0.50
	When I select Gherkin
	Then the vending machine should display INSERT £0.50
	When I enter £0.25 into the machine
	When I enter £0.50 into the machine
	Then the vending machine should give me my Gherkin
	And there should be £0.25 change in the coin tray