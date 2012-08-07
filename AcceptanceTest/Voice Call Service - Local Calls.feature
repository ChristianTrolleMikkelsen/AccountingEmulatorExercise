Feature: Voice Call Service - Local Calls
	In order to make a profit on local voice calls
	As an accountant
	I must be able to register voice calls 
	and I must be able to choose between different kinds of charges for voice calls

Scenario Outline: Call Charge
	Given I have a registered a Voice Call
	And the supscription has "DKK" as local country
	And the call is from "DKK"
	And the call is made to "DKK"
	And I have specified a local call charge of <CallCharge>
	When I calculate the price of a call to be: <Cost>
	Then an the initial call cost must be added to the calculated price
	And the final cost must be: <CostWithCharge>

	Examples: 
	| CallCharge | Cost  | CostWithCharge |
	| 1.0        | 0.29  | 1.29           |
	| 1.0        | 24.30 | 25.30          |
	| 2.0        | 1.45  | 2.45           |


Scenario Outline: Second Charge
	Given I have a registered a Voice Call
	And the supscription has "DKK" as local country
	And the call is from "DKK"
	And the call is made to "DKK"
	And I have specified a second charge of: <Charge>
	And the call lasted: <Duration>
	When I calculate the price of a call
	Then I must be able to calculate the price of the call by each started second of the duration of the call
	And the price must be: <Cost>

	Examples: 
	| Charge | Duration | Cost |
	| 1.0    | 00:00:30 | 30   |
	| 2.0    | 00:00:30 | 60   |
	| 0.5    | 00:01:00 | 30   |


Scenario Outline: Minute Charge
	Given I have a registered a Voice Call
	And the supscription has "DKK" as local country
	And the call is from "DKK"
	And the call is made to "DKK"
	And I have specified a minute charge of: <Charge>
	And the call lasted: <Duration>
	When I calculate the price of a call
	Then I must be able to calculate the price of the call by each started minute of the duration of the call
	And the price must be: <Cost>

	Examples: 
	| Charge | Duration | Cost |
	| 1.0    | 00:00:30 | 1    |
	| 2.0    | 00:00:30 | 2    |
	| 1.0    | 00:01:00 | 2    |


Scenario Outline: Interval Charge
	Given I have a registered a Voice Call
	And the Voice Call is supscription has "DKK" as local country
	And the call is from "DKK"
	And the call is made to "DKK"
	And the Voice Call Service is set to charge <Charge> for every <Interval> begun
	And the Voice Call lasted <Duration>
	When I calculate the price of a call
	Then I must be able to calculate the price of the call by each started <Interval> of the duration of the call
	And the price must be: <Cost>

	Examples: 
	| Interval | Charge | Duration | Cost |
	| 00:00:30 | 1.0    | 00:00:29 | 1    |
	| 00:00:30 | 1.0    | 00:00:30 | 2    |
	| 00:00:45 | 0.9    | 00:00:45 | 0.9  |

