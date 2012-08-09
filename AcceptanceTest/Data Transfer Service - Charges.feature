Feature: Data Transfer Service - Charges
	In order to make a profit on data transfer calls (local and foreign)
	As an accountant
	I must be able to register data transfer calls 
	and I must be able to choose between different kinds of charges for data transfer calls


Scenario Outline: Pr. Kilobyte Charge
	Given I have a subscription which has "DK" as local country
	And I have added a Data Transfer service to the subscription
	And I have specified a charge of: "<KilobyteCharge>" for every kilobyte for the Data Transfer service
	And I have added Data Transfer charge of "<ForeignKilobyteCharge>"  for every kilobyte for calling to or from country: "DE"
	And I start a Data Transfer from "<Url>"
	And the Data Transfer starts in "<SourceCountry>"
	And the Data Transfer ends in "<DestinationCountry>"
	And the Data Transfer sizes up to: "<TransferSizeInKilobytes>" kilobytes
	When the Data Transfer has been completed
	And the accounting machine has processed the data transfer call
	Then the price for performing the Data Transfer must be: "<Cost>"

	Examples: 
	| KilobyteCharge | ForeignKilobyteCharge | Url            | TransferSizeInKilobytes | Cost | SourceCountry | DestinationCountry |
	| 10.0           | 40                    | www.google.com | 10                      | 100  | DK            | DK                 |
	| 20.0           | 40                    | www.google.com | 10                      | 200  | DK            | DK                 |
	| 30.0           | 40                    | www.google.com | 10                      | 300  | DK            | DK                 |
	| 10.0           | 40                    | www.google.com | 10                      | 500  | DK            | DE                 |
	| 20.0           | 40                    | www.google.com | 10                      | 600  | DE            | DK                 |
	| 30.0           | 40                    | www.google.com | 10                      | 400  | DE            | DE                 |


Scenario Outline: Pr. Megabyte Charge
	Given I have a subscription which has "DK" as local country
	And I have added a Data Transfer service to the subscription
	And I have specified a charge of: "<MegabyteCharge>" for every megabyte for the Data Transfer service
	And I have added Data Transfer charge of "<ForeignMegabyteCharge>"  for every megabyte for calling to or from country: "DE"
	And I start a Data Transfer from "<Url>"
	And the Data Transfer starts in "<SourceCountry>"
	And the Data Transfer ends in "<DestinationCountry>"
	And the Data Transfer sizes up to: "<TransferSizeInMegabytes>" megabytes
	When the Data Transfer has been completed
	And the accounting machine has processed the data transfer call
	Then the price for performing the Data Transfer must be: "<Cost>"

	Examples: 
	| MegabyteCharge | ForeignMegabyteCharge | Url            | TransferSizeInMegabytes | Cost | SourceCountry | DestinationCountry |
	| 10.0           | 40                    | www.google.com | 10                      | 100  | DK            | DK                 |
	| 20.0           | 40                    | www.google.com | 10                      | 200  | DK            | DK                 |
	| 30.0           | 40                    | www.google.com | 10                      | 300  | DK            | DK                 |
	| 10.0           | 40                    | www.google.com | 10                      | 500  | DK            | DE                 |
	| 20.0           | 40                    | www.google.com | 10                      | 600  | DE            | DK                 |
	| 30.0           | 40                    | www.google.com | 10                      | 400  | DE            | DE                 |

