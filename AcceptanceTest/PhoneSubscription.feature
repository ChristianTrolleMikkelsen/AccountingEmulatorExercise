Feature: Phone Subscription
	In order to make a better profit on phones
	As an accountant
	I want to be able to define subscriptions which consists of one or more services
	And register the usage of these services


Scenario: Create A New Subscription
	Given I want to create a new subscription for phone "51948896"
	When I have created the subscription
	Then the subscription is registered for phone "51948896"
	And the subscription has default local country "DKK"
	And the subscription contains an empty list of services


Scenario: Can add a service to a subscription
	Given I have created a subscription for phone "51948896"
	When I add a new Voice Call service to the subcription
	Then the Voice Call service must be added to the list of services


Scenario: Cannot add a service to a subscription with existing service of same type
	Given I have created a subscription for phone "51948896"
	And the subscription allready contains a Voice Call service
	When I add a new Voice Call service to the subscription
	Then the Voice Call service is not added to the list of services

	
Scenario: Create A New Subscription With Local Country
	Given I want to create a new subscription for phone "51948896"
	And I want the local country of the subscription to be "USD"
	When I have created the subscription with a not default country
	Then the subscription is registered for phone "51948896"
	And the subscription has local country "USD"
	And the subscription contains an empty list of services
