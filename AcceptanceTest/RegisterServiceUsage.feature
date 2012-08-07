Feature: Register Service Usage
	In order to calculate the bill for a customers usage of one or more phone subscriptions
	As an accountant
	I must be able to inspect all registered serivce usage of the subscriptions

Scenario: Register Service Usage For Several Subscriptions
	Given a customer has 2 phone subscriptions
	And the customer makes use of all the services provided by both of the subscriptions
	When I want to create a bill for the subscriptions
	Then I can inspect every single use of a given service in a subscription

