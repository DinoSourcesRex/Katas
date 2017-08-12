Feature: CancelProduct
	In order to support a customer changing their mind
	As a vendor
	I want to be able to cancel and order and return the money

Scenario: Cancel a transaction
	Given that a Chocolate costs a total of £1
	When I enter £0.50 into the machine
	And I cancel my purchase
	Then there should be £0.50 change in the coin tray
	And the vending machine should display INSERT COIN