Feature: Data Transfer Service - Register Call
	In order to make a profit on data calls
	As an accountant
	I must be able to register data calls by a set of parameters to calculate the bill for a customer

Scenario Outline: Register Local Data Transfer Call
	Given a subscription with phone number "77889955" exists
	And the subscription includes the Data Transfer Service
	And the customer makes a data transfer at "<StartTime>"
	And the "<Size>" kb of data is transfered
	And the data transfer is from or to: "<Url>"
	And the data transfer is made from: "<SourceCountry>"
	And the data transfer is made to: "<DestinationCountry>"
	When the data transfer ends
	Then I must be able to find the data transfer using the subscription 
	And the start time of the data transfer must be registered at "<StartTime>"
	And the size of the data transfer must be registered to be: "<Size>" kb
	And the source of the data transfer must be registered as "<Url>"
	And the country from which the data transfer was made must be registered as "<SourceCountry>"
	And the country for which the data transfer was made to must be registered as "<DestinationCountry>"

	Examples: 
	| StartTime | Size | Url			   | SourceCountry | DestinationCountry |
	| 09:00:00  | 1234 | www.google.com    | DK            | DK                 |
	| 15:41:02  | 4321 | www.telenor.dk    | DK            | DK                 |
	| 18:29:56  | 1029 | www.wikipedia.com | DK            | DK                 |


Scenario Outline: Register Foreign Voice Call
	Given a subscription with phone number "77889955" exists
	And the subscription includes the Data Transfer Service
	And the subscription includes support for transfering data from country: "<SourceCountry>"
	And the subscription includes support for transfering data to country: "<DestinationCountry>"
	And the customer makes a data transfer at "<StartTime>"
	And the "<Size>" kb of data is transfered
	And the data transfer is from or to: "<Url>"
	And the data transfer is made from: "<SourceCountry>"
	And the data transfer is made to: "<DestinationCountry>"
	When the data transfer ends
	Then I must be able to find the data transfer using the subscription 
	And the start time of the data transfer must be registered at "<StartTime>"
	And the size of the data transfer must be registered to be: "<Size>" kb
	And the source of the data transfer must be registered as "<Url>"
	And the country from which the data transfer was made must be registered as "<SourceCountry>"
	And the country for which the data transfer was made to must be registered as "<DestinationCountry>"

	Examples: 
	| StartTime | Size | Url			   | SourceCountry | DestinationCountry |
	| 09:00:00  | 1234 | www.google.com    | DK            | DE                 |
	| 15:41:02  | 4321 | www.telenor.dk    | DE            | DK                 |
	| 18:29:56  | 1029 | www.wikipedia.com | DE            | DE                 |


