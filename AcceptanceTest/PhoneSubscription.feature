Feature: Phone Subscription
	In order to make a better profit on phones
	As an accountant
	I want to be able to define subscriptions which consists of one or more services
	And register the usage of these services


Scenario: Create A New Subscription
	Given I want to create a new subscription for phone "23458126"
	When I have created the subscription
	Then the subscription is registered for phone "23458126"
	And the subscription has default local country "DK"
	And the subscription contains an empty list of services


Scenario: Can add a service to a subscription
	Given I have created a subscription for phone "23458126"
	When I add a new Voice Call service to the subcription
	Then the Voice Call service must be added to the list of services


Scenario: Create A New Subscription With Local Country
	Given I want to create a new subscription for phone "23458126"
	And I want the local country of the subscription to be "USD"
	When I have created the subscription with a not default country
	Then the subscription is registered for phone "23458126"
	And the subscription has local country "USD"
	And the subscription contains an empty list of services


Scenario: Find all calls related to a subscription
	Given I have created a subscription for phone "23458126"
	And the subscription includes a Voice Call service
	When I make a Voice Call with the phone "23458126"
	Then I can find the call using the subscription phone number
