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

Scenario: Validate Extraction Rule Mask Size must be a percentage figure
	And I click on the masking checkbox
    And I update the Masking field:
	| Masking Field  | Error Value                     |
	| Left,101       | Value must be between 1 and 100 |
	| Left,0.99      | Value must be between 1 and 100 |
	| Left, sometext | Value must be numeric           |
	| Right, -1      | Value must be between 1 and 100 |
	
Scenario: Validate cannot add an Extraction Rule without a Pattern
	And I update the Add Extraction Rule field 'Type' with the drop-down value of 'Content Pattern Match'
    When I save the Extraction Rule
	Then I should get an error message for the following fields:
	| Field   | Error Text                                                                                    |
	| Pattern | A valid regular expression is required. CR and LF should be matched using \r and \\n patterns |

Scenario: Validate name field character length cannot exceed 50 characters
	And I enter the Add Extraction Rule field 'Name' with the text 'Lorem ipsum dolor sit amet, consectetur adipiscinga'
    When I save the Extraction Rule
	Then I should get an error message for the following fields:
	| Field | Error Text                 |
	| Name  | Max character length is 50 |

Scenario: Validate that you cannot add an Extraction Rule that already exists
	And I enter the Add Extraction Rule field 'SearchName' with the text '_Animals'
    When I save the Extraction Rule
	Then I should get an error message for the following fields:
	| Field | Error Text							|
	| Name  | Extraction rule name must be unique.	|
