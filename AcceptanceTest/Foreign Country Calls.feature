Feature: Foreign Country Calls
	In order to make a better profit on phones
	As an accountant
	I want my subscribes be charged differently depending on which country they are calling from and to


Scenario: Calling from local country to foreign country
	Given I have a subscription for phone "23458126" with local country "DK"
	And the subscription is allowed to perform Voice Calls to "DE"
	And a Voice Call is performed from "DK" to "DE"
	When the cost of the call is calculated
	Then the charge is the sum of both calls

Scenario: Calling to local country from foreign country
	Given I have a subscription for phone "23458126" with local country "DK"
	And the subscription is allowed to perform Voice Calls to "DE"
	And a Voice Call is performed from "DE" to "DK"
	When the cost of the call is calculated
	Then the charge is the sum of both calls

Scenario: Calling to from foreign country to foreign country
	Given I have a subscription for phone "23458126" with local country "DK"
	And the subscription is allowed to perform Voice Calls to "DE"
	And a Voice Call is performed from "DE" to "DE"
	When the cost of the call is calculated
	Then the charge is that of the foreign country only