Feature: SMS Service - Register Call
	In order to make a profit on sms calls
	As an accountant
	I must be able to register sms calls by a set of parameters to calculate the bill for a customer

Scenario Outline: Register Local SMS Call
	Given a subscription with phone number "77889955" exists
	And the subscription includes the SMS Service
	And the customer creates an SMS
	And the SMS has the lenght "<Lenght>" characters
	And the SMS is sent to number: "<Receiver>"
	And the SMS is sent from: "<SourceCountry>"
	And the SMS is sent to: "<DestinationCountry>"
	When the SMS is sent at "<SentTime>"
	Then I must be able to find the SMS using the subscription 
	And the start time of the SMS must be registered at "<SentTime>"
	And the lenght of the SMS must be registered to be "<Lenght>" characters
	And the receiver of the SMS must be registered as "<Receiver>"
	And the country from which the SMS was sent from must be registered as "<SourceCountry>"
	And the country for which the SMS was sent to must be registered as "<DestinationCountry>"

	Examples: 
	| SentTime | Lenght | Receiver | SourceCountry | DestinationCountry |
	| 09:00:00 | 128	| 98561234 | DK            | DK                 |
	| 15:41:02 | 256    | 23458126 | DK            | DK                 |
	| 18:29:56 | 512    | 98561234 | DK            | DK                 |


Scenario Outline: Register Foreign SMS Call
	Given a subscription with phone number "77889955" exists
	And the subscription includes the SMS Service
	And the subscription includes support for texting from country: "<SourceCountry>"
	And the subscription includes support for texting to country: "<DestinationCountry>"
	And the customer creates an SMS
	And the SMS has the lenght "<Lenght>" characters
	And the SMS is sent to number: "<Receiver>"
	And the SMS is sent from: "<SourceCountry>"
	And the SMS is sent to: "<DestinationCountry>"
	When the SMS is sent at "<SentTime>"
	Then I must be able to find the SMS using the subscription 
	And the start time of the SMS must be registered at "<SentTime>"
	And the lenght of the SMS must be registered to be "<Lenght>" characters
	And the receiver of the SMS must be registered as "<Receiver>"
	And the country from which the SMS was sent from must be registered as "<SourceCountry>"
	And the country for which the SMS was sent to must be registered as "<DestinationCountry>"

	Examples: 
	| SentTime | Lenght | Receiver | SourceCountry | DestinationCountry |
	| 09:00:00 | 128	| 98561234 | DK            | DE                 |
	| 15:41:02 | 256    | 23458126 | DE            | DK                 |
	| 18:29:56 | 512    | 98561234 | DE            | DE                 |

