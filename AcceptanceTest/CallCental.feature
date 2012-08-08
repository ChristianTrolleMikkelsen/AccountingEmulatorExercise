Feature: Call Central
	In order to calculate the bill for a customers usage of one or more phone subscriptions
	As an accountant
	I want all calls to be registred at the Call Central so that I am able to inspect all registered serivce usage of a subscription

Scenario: Register a Call
	Given a customer has a phone subscription with the Voice Call Service
	When the customer makes a Voice Call with the phone
	Then the call has been registred at the Call Central

Scenario: Deny usage of services not provided by the subscription
	Given a customer has a phone subscriptions without any services
	When the customer tries to make a Voice Call with the phone
	Then the service is denied when contacting the Call Central

Scenario: Deny usage of services if the call is outside the allow country range
	Given a customer has a phone subscription with the Voice Call Service
	When the customer tries to make a Voice Call with the phone to "DE" from "DK"
	Then the service is denied when contacting the Call Central
