Feature: Customer Phone Subscription
	In order to fullfill the needs of our customers
	As a salesman
	I must be able to add phone subscriptions to a customer

Scenario: Customer Phone Subscriptions
	Given I have created a new customer
	When I sell the customer a phone subscription
	Then the phone subscription is added to the customers inventory
	