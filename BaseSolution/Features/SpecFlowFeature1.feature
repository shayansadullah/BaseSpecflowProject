Feature: ValidateExtractionRules
	In order to create a successful extraction rule
	As an end user
	I want to be able to perform an extraction of data

@mytag
Scenario: Perform An Extraction
    Given I have navigated to 'demo.activenavigation.com'
	And I click on 'Add Extraction Rule' link
	And I enter the Add Extraction Rule field 'Name' with the text 'Any Rule Name'
	And I update the Add Extraction Rule field 'Type' with the drop-down value of 'Content Pattern Match'
	And I update the Add Extraction Rule field 'Data Type' with the drop-down value of 'String'
	And I enter the Add Extraction Rule field 'Description' with the text 'Any description'
	And I update the Add Extraction Rule field 'Masking' with values 'Left,20'
	And I enter the Add Extraction Rule field 'Pattern' with the text '(?<=\\)[M-Z][^\\]*$'
	When I save the Extraction Rule
	And I search for the Extraction Rule 'Any Rule Name'
	Then Extraction Rule 'Any Rule Name' is present