Feature: ValidateExtractionRules
	In order to validate an extraction rule
	As an end user
	I want to be able to enter incorrect data so that the validation is prompted

Background:
    Given I have navigated to 'demo.activenavigation.com'
	And I click on 'Add Extraction Rule' link

Scenario: Validate Extraction Rule input form by not entering any data
	When I save the Extraction Rule without any data input
	Then I should get an error message stating that the fields are required for the following:
	| Field     |
	| Name      |
	| Type      |
	| Data Type |
	And I update the Add Extraction Rule field 'Masking' with values 'Please select,'
	When I save the Extraction Rule
	Then I should get an error message for the following fields:
	| Field			| Masking Error Text																|
	| Part to mask	| You must select which part of the extraction rule value you would like to mask.	|
	| Mask size (%)	| You must select a percentage of the value to be masked between 1 and 100.			|

@ignore
Scenario Outline: Validate Extraction Rule input form by not filling in the Mask size correctly
	When I click on the masking checkbox
	And I update the Add Extraction Rule field 'Masking' with values '<MaskingDetails>'
    Then I should get an error message '<MaskingErrorText>'

	Examples:
    | MaskingDetails | Masking Error Text	|
    | Left,101       |						|


@ignore
Scenario Outline: Validate cannot add an Extraction Rule without a Pattern
	And I enter the Add Extraction Rule field 'Name' with the text '<NameText>'
	And I update the Add Extraction Rule field 'Type' with the drop-down value of '<TypeText>'
	And I update the Add Extraction Rule field 'Data Type' with the drop-down value of '<DataType>'
	And I update the Add Extraction Rule field 'Masking' with values '<MaskingDetails>'
	And I enter the Add Extraction Rule field 'Pattern' with the text '<PatternValue>'
    When I save the Extraction Rule
	Then I should get an error message for the following fields:
	| Field			| Error Text									|	
	| Pattern       |  "A valid regular expression is required"     |	

    Examples:
	| NameText				   | TypeText					| DataType | Description				 | MaskingDetails | PatternValue		|
	| No Pattern input         | Content Pattern Match		| String   | some description            |	Left,20		  |						|

