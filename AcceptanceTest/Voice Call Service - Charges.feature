Feature: Voice Call Service - Charges
	In order to make a profit on voice calls (local and foreign)
	As an accountant
	I must be able to register voice calls 
	and I must be able to choose between different kinds of charges for voice calls

Scenario Outline: Call Charge

	Given I have a subscription which has "DK" as local country
	And I have added a Voice Call service to the subscription
	And I have specified a call charge of "<CallCharge>"
	And I have added call charge of "<ForeignCallCharge>" for calling to or from country: "DE"
	And start a call from "<SourceCountry>"
	And the call is made to "<DestinationCountry>"
	And the call lasts: "<Duration>"
	When the call is completed
	And the accounting machine has processed the call
	Then the price must be: "<Cost>"

	Examples: 
	| CallCharge | ForeignCallCharge | Duration | Cost | SourceCountry | DestinationCountry |
	| 1.0        | 0                 | 00:00:29 | 1    | DK            | DK                 |
	| 2.0        | 0                 | 00:01:30 | 2    | DK            | DK                 |
	| 1.0        | 0                 | 02:02:00 | 1    | DK            | DK                 | 
	| 1.0        | 4                 | 00:00:29 | 4    | DE            | DK                 |
	| 2.0        | 3                 | 00:01:30 | 3    | DK            | DE                 |
	| 1.0        | 2.3               | 02:02:00 | 2.3  | DE            | DE                 | 

Scenario Outline: Second Charge
	Given I have a subscription which has "DK" as local country
	And I have added a Voice Call service to the subscription
	And I have specified a second charge of: "<Charge>" for the Voice Call service
	And I have added second charge of "<ForeignCallCharge>" for calling to or from country: "DE"
	And start a call from "<SourceCountry>"
	And the call is made to "<DestinationCountry>"
	And the call lasts: "<Duration>"
	When the call is completed
	And the accounting machine has processed the call
	Then the price must be: "<Cost>"

	Examples: 
	| Charge | ForeignCallCharge | Duration | Cost | SourceCountry | DestinationCountry |
	| 1.0    | 0                 | 00:00:30 | 30   | DK            | DK                 |
	| 2.0    | 0                 | 00:00:30 | 60   | DK            | DK                 |
	| 0.5    | 0                 | 00:01:00 | 30   | DK            | DK                 |
	| 1.0    | 3                 | 00:00:30 | 90   | DK            | DE                 |
	| 2.0    | 4                 | 00:00:30 | 120  | DE            | DK                 |
	| 0.5    | 5                 | 00:01:00 | 300  | DE            | DE                 |


Scenario Outline: Minute Charge
	Given I have a subscription which has "DK" as local country
	And I have added a Voice Call service to the subscription
	And I have specified a minute charge of: "<Charge>" for every minute begun for the Voice Call service
	And I have added minute charge of "<ForeignCallCharge>" for calling to or from country: "DE"
	And start a call from "<SourceCountry>"
	And the call is made to "<DestinationCountry>"
	And the call lasts: "<Duration>"
	When the call is completed
	And the accounting machine has processed the call
	Then the price must be: "<Cost>"

	Examples: 
	| Charge | ForeignCallCharge | Duration | Cost | SourceCountry | DestinationCountry |
	| 1.0    | 10                | 00:00:30 | 1    | DK            | DK                 |
	| 2.0    | 10                | 00:00:30 | 2    | DK            | DK                 |
	| 1.0    | 10                | 00:01:01 | 2    | DK            | DK                 |
	| 1.0    | 3                 | 00:00:30 | 3    | DK            | DE                 |
	| 2.0    | 4                 | 00:00:30 | 4    | DE            | DK                 |
	| 0.5    | 5                 | 00:01:01 | 10   | DE            | DE                 |


Scenario Outline: Interval Charge
	Given I have a subscription which has "DK" as local country
	And I have added a Voice Call service to the subscription
	And I have specified a interval charge of: "<Charge>" for every "<Interval>" begun for the Voice Call service
	And I have added an interval charge of "<ForeignCallCharge>" for every "<ForeignInterval>" for calling to or from country: "DE"
	And start a call from "<SourceCountry>"
	And the call is made to "<DestinationCountry>"
	And the call lasts: "<Duration>"
	When the call is completed
	And the accounting machine has processed the call
	Then the price must be: "<Cost>"

	Examples: 
	| Interval | Charge | ForeignInterval | ForeignCallCharge | Duration | Cost | SourceCountry | DestinationCountry |
	| 00:00:30 | 1.0    | 00:00:45        | 10                | 00:00:29 | 1    | DK            | DK                 |
	| 00:00:30 | 1.0    | 00:00:45        | 10                | 00:00:31 | 2    | DK            | DK                 |
	| 00:00:45 | 0.9    | 00:00:45        | 10                | 00:00:45 | 0.9  | DK            | DK                 |
	| 00:00:30 | 1.0    | 00:00:30        | 5                 | 00:00:29 | 5    | DK            | DE                 |
	| 00:00:30 | 1.0    | 00:00:45        | 10                | 00:00:31 | 10   | DE            | DK                 |
	| 00:00:45 | 0.9    | 00:00:55        | 15                | 00:01:55 | 45   | DE            | DE                 |

