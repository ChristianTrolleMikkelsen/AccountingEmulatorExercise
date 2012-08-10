Feature: Customer Subscription
	In order to fullfill the needs of our customers
	As a salesman
	I must be able to add phone subscriptions to a customer

Scenario: Customer Subscriptions
	Given I have created a new customer
	When I sell the customer a phone subscription
	Then the phone subscription is assigned the customer
	And the customer has the initial customer status NORMAL
	