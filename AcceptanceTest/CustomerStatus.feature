Feature: Customer Status
	In order to make a better profit
	As a salesman
	I want to be able to change the status of a customer so they may receive a discount

Scenario Outline: Customer Status Discount
	Given I have given a customer the "<CustomerStatus>" status
	And I charge <CalculatedBill> pr Voice Call
	And the customer has finished a call
	When I calculate the cost of the customers bill to <CalculatedBill>
	Then bill receives a <Discount>% discount
	And the final bill is <BillWithDiscount>

	Examples: 
	| CustomerStatus | Discount | CalculatedBill | BillWithDiscount |
	| Normal         | 0	    | 100            | 100              |
	| HighRoller     | 10	    | 100            | 90               |
	| VIP            | 20	    | 100            | 80               |