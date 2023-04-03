Feature: StandardGiftCardsCartTotalAmountValidation

Background:
Given I navigate to 'https://www.amazon.com/'
	Then I search 'Gift Cards' 
	And I choose the gift card by type name 'EGiftCards'
	Then I wait load page state 'NetworkIdle'
	And I click to button by name 'Standard'
	And I click to button by name 'Amazon Logo'

@positive
Scenario: validate gift card total amount with custom amount
	Then I enter gift card details
	| Custom Amount | Delivery Email          | From    | Message    | Quantity | Delivery Date |
	| 324           | schepetkov.dm@gmail.com | Dmitriy | Well Done! | 2        | Today         |
	And I click to button by name 'Add to cart'
	Then I wait load page state 'NetworkIdle'
	And I validate cart total amount

@positive
Scenario: validate gift card total amount with fix sum via button 25$
	Then I enter gift card details
	| Amount | Delivery Email          | From    | Message    | Quantity | Delivery Date |
	| 25     | schepetkov.dm@gmail.com | Dmitriy | Well Done! | 8        | Today         |
	And I click to button by name 'Add to cart'
	Then I wait load page state 'NetworkIdle'
	And I validate cart total amount

@positive
Scenario: validate gift card total amount with fix sum via button 50$
	Then I enter gift card details
	| Amount | Delivery Email          | From    | Message    | Quantity | Delivery Date |
	| 50     | schepetkov.dm@gmail.com | Dmitriy | Well Done! | 4        | Today         |
	And I click to button by name 'Add to cart'
	Then I wait load page state 'NetworkIdle' 
	And I validate cart total amount

@positive
Scenario: validate gift card total amount with fix sum via button 75$
	Then I enter gift card details
	| Amount | Delivery Email          | From    | Message    | Quantity | Delivery Date |
	| 75     | schepetkov.dm@gmail.com | Dmitriy | Well Done! | 3        | Today         |
	And I click to button by name 'Add to cart'
	Then I wait load page state 'NetworkIdle'
	And I validate cart total amount

@positive
Scenario: validate gift card total amount with fix sum via button 100$
	Then I enter gift card details
	| Amount | Delivery Email          | From    | Message    | Quantity | Delivery Date |
	| 100    | schepetkov.dm@gmail.com | Dmitriy | Well Done! | 5        | Today         |
	And I click to button by name 'Add to cart'
	Then I wait load page state 'NetworkIdle' 
	And I validate cart total amount