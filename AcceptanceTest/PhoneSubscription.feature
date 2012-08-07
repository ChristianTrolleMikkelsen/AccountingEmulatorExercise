Feature: Phone Subscription
	In order to make a better profit on phones
	As an accountant
	I want to be able to define subscriptions which consists of one or more services
	And register the usage of these services

Scenario: Create A New Subscription
	Given I want to create a new subscription
	When I have created the subscription
	Then the subscription always consists of an empty list of Inland services
	And an empty list of Abroad services

Scenario: Can add a service to a subscription
	Given I have a subscription
	When I add a new Voice Call service to the Inland services
	And I add a new Voice Call service to the Abroad services
	Then the Voice Call service must be added the Inland services
	And the Voice Call service must be added the Abroad services

Scenario: Cannot add a service to a subscription with existing service of same type
	Given I have a subscription
	And the Inland services allready contains a Voice Call service
	And the Abroad services allready contains a Voice Call service
	When I add a new Voice Call service to the Inland services
	And I add a new Voice Call service to the Abroad services
	Then the Voice Call service is not added the Inland services
	And the Voice Call service is not added the Abroad services

	

