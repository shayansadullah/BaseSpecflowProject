Feature: ValidateExtractionRules
	In order to create a successful extraction rule
	As an end user
	I want to be able to perform an extraction of data

Background:
    Given I have navigated to 'demo.activenavigation.com'
	And I click on 'Add Extraction Rule' link

Scenario Outline: Add Extraction Rule Successfully
	And I enter the Add Extraction Rule field 'Name' with the text '<NameText>'
	And I update the Add Extraction Rule field 'Type' with the drop-down value of '<TypeText>'
	And I update the Add Extraction Rule field 'Data Type' with the drop-down value of '<DataType>'
	And I enter the Add Extraction Rule field 'Description' with the text '<Description>'
	And I update the Add Extraction Rule field 'Masking' with values '<MaskingDetails>'
	And I enter the Add Extraction Rule field 'Pattern' with the text '<PatternValue>'
	When I save the Extraction Rule successfully
	And I search for the Extraction Rule '<NameText>'
	Then Extraction Rule '<NameText>' is present

    Examples:
	| NameText				| TypeText					| DataType | Description				 | MaskingDetails | PatternValue	    |
	| Any Rule Name         | Content Pattern Match		| String   | some description            |	Left,20		  |	(?<=\\)[M-Z][^\\]*$ |


Scenario: Validate Extraction Rule input form
	Given I save the Extraction Rule without any data
	Then I should get an error message for the following fields:
	| Field     |
	| Name      |
	| Type      |
	| Data Type |
