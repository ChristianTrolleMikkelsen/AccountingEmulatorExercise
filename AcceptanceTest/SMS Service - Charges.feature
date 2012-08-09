Feature: SMS Service - Charges
	In order to make a profit on sms calls (local and foreign)
	As an accountant
	I must be able to register sms calls 
	and I must be able to choose between different kinds of charges for sms calls

Scenario Outline: Call Charge
	Given I have a subscription which has "DK" as local country
	And I have added a SMS service to the subscription
	And I have specified a SMS send charge of "<SendCharge>"
	And I have added SMS send charge of "<ForeignSendCharge>" for calling to or from country: "DE"
	And I create a SMS of "<SMSLenght>" characters
	And the SMS is sent from "<SourceCountry>"
	And the SMS is sent to "<DestinationCountry>"
	When the SMS has been sent
	And the accounting machine has processed the sms call
	Then the price for sending the sms must be: "<Cost>"

	Examples: 
	| SendCharge | ForeignSendCharge | SMSLenght | Cost | SourceCountry | DestinationCountry |
	| 1.0        | 0                 | 128		 | 1    | DK            | DK                 |
	| 2.0        | 0                 | 256       | 2    | DK            | DK                 |
	| 1.0        | 0                 | 512       | 1    | DK            | DK                 | 
	| 1.0        | 4                 | 64        | 4    | DE            | DK                 |
	| 2.0        | 3                 | 32        | 3    | DK            | DE                 |
	| 1.0        | 2.3               | 16        | 2.3  | DE            | DE                 | 

Scenario Outline: Lenght Charge
	Given I have a subscription which has "DK" as local country
	And I have added a SMS service to the subscription
	And I have specified a lenght charge of: "<LenghtCharge>" for every "<NumberOfCharacters>" character for the SMS Call service
	And I have added SMS lenght charge of "<ForeignLenghtCharge>"  for every "<ForeignNumberOfCharacters>" for calling to or from country: "DE"
	And I create a SMS of "<SMSLenght>" characters
	And the SMS is sent from "<SourceCountry>"
	And the SMS is sent to "<DestinationCountry>"
	When the SMS has been sent
	And the accounting machine has processed the sms call
	Then the price for sending the sms must be: "<Cost>"

	Examples: 
	| LenghtCharge | NumberOfCharacters | ForeignLenghtCharge | ForeignNumberOfCharacters | SMSLenght | Cost | SourceCountry | DestinationCountry |
	| 1.0          | 16                 | 1.0                 | 512                       | 64        | 4    | DK            | DK                 |
	| 2.0          | 32                 | 2.0                 | 256                       | 128       | 8    | DK            | DK                 |
	| 1.0          | 64                 | 3.0                 | 128                       | 256       | 4    | DK            | DK                 |
	| 1.0          | 128                | 4.0                 | 64                        | 64        | 4    | DK            | DE                 |
	| 2.0          | 256                | 5.0                 | 32                        | 256       | 40   | DE            | DK                 |
	| 1.0          | 512                | 6.0                 | 16                        | 321       | 120  | DE            | DE                 |


