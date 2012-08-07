Feature: Voice Call Service
	In order to make a profit on voice calls
	As an accountant
	I must be able to register voice calls by a set of parameters to calculate the bill for a customer

Scenario Outline: Register Voice Call
	Given a customer has a subscription linked to a mobile phone with the phone number "77889955"
	And the subscription includes the Voice Call Service
	And the customer makes a Voice Call at <StartTime>
	And the call lasts <Duration>
	And the call is made to number: <Receiver>
	And the call is made from: <SourceCountry>
	And the call is made to: <DestinationCountry>
	When the call ends
	Then I must be able to find the call using the subscription 
	And the start time of the call must be registered at <StartTime>
	And the duration of the call must be registered to have lasted <Duration>
	And the receiver of the call must be registered as <Receiver>
	And the country from which the call was made must be registered as <SourceCountry>
	And the country for which the call was made to must be registered as <DestinationCountry>

	Examples: 
	| StartTime | Duration | Receiver | SourceCountry | DestinationCountry |
	| 09:00:00  | 01:37    | 27206617 | DK            | DE                 |
	| 15:41:02  | 02:15    | 51948896 | DK            | DK                 |
	| 18:29:56  | 28:09    | 27206617 | DE            | US                 |

