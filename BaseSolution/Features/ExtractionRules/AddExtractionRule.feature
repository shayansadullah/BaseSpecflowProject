Feature: AddExtractionRules
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
	And I search for the Extraction Rule
	Then Extraction Rule is present

    Examples:
	| NameText                            | TypeText              | DataType | Description         | MaskingDetails | PatternValue        |
	| DataExtraction                      | Content Pattern Match | String   | some description    | Left,20        | (?<=\\)[M-Z][^\\]*$ |
	| ÝÞßàáâãäåæçèéêëìíîï                 | Content Pattern Match | String   | ÝÞßàáâãäåæçèéêëìíîï | Left,20        | some pattern        |
	| 49characters49characters49characte  | Content Pattern Match | String   | some description    | Right,80       | (?<=\\)[M-Z][^\\]*$ |
	| 50characters50characters50character | Content Pattern Match | String   | some description    | Middle,40      | (?<=\\)[M-Z][^\\]*$ |