Feature: StandardGiftCardsAmountValidation

Background:
Given I navigate to application
	And I click on the gift cards tab
	And I click on the picture eGif card
	And Fill in To 'shepetkov.dm@gmail.com'
	And Fill in From 'Dmitriy'
	And Fill in Message 'My message'

@AmountCheck @Standard @Positive
Scenario: Add to cart gift card with email delivery with custom ammount
	And I choose the amount '345'$
	And Fill in Quantity '1'
	Then I'm adding a gift card to my cart

@AmountCheck @Standard @Positive
Scenario: Add to cart gift card with email delivery with custom ammount (total amount check)
	And I choose the amount '453'$
	And Fill in Quantity '2'
	Then I'm adding a gift card to my cart

@AmountCheck @Standard @Positive
Scenario: Add to cart gift card with email delivery with fixed amount 25$
	And I choose the amount '25'$
	And Fill in Quantity '1'
	Then I'm adding a gift card to my cart

@AmountCheck @Standard @Positive
Scenario: Add to cart gift card with email delivery with fixed amount 50$
	And I choose the amount '50'$
	And Fill in Quantity '1'
	Then I'm adding a gift card to my cart

@AmountCheck @Standard @Positive
Scenario: Add to cart gift card with email delivery with fixed amount 75$
	And I choose the amount '75'$
	And Fill in Quantity '1'
	Then I'm adding a gift card to my cart

@AmountCheck @Standard @Positive
Scenario: Add to cart gift card with email delivery with fixed amount 100$
	And I choose the amount '100'$
	And Fill in Quantity '1'
	Then I'm adding a gift card to my cart