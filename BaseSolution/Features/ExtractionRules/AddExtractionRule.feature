Feature: AddExtractionRules
	In order to create an Extraction Rule
	As an end user
	I want to be able to create a Rule with valid data

Background:
    Given the user navigates to the 'Active Navigation' site
	And the user opens the 'Add Extraction Rule' section

Scenario Outline: Add Extraction Rule Successfully
    Given the User updates the Extraction Rule with <NameText>,<TypeText>,<DataType>,<Description>,<PartToMask>,<MaskSize>,<PatternValue>	
	And that User saves the Extraction Rule
	When that User performs a search for that Extraction Rule
	Then the Extraction Rule created is present

    Examples:
	| NameText                            | TypeText              | DataType | Description         | PartToMask | MaskSize	|  PatternValue			|
	| DataExtraction                      | Content Pattern Match | String   | some description    | Left       | 20		| (?<=\\)[M-Z][^\\]*$	|
	| ÝÞßàáâãäåæçèéêëìíîï                 | Content Pattern Match | String   | ÝÞßàáâãäåæçèéêëìíîï | Left       | 20		| some pattern			|
	| 49characters49characters49characte  | Content Pattern Match | String   | some description    | Right      | 80		| (?<=\\)[M-Z][^\\]*$	|
	| 50characters50characters50character | Content Pattern Match | String   | some description    | Middle     | 40		| (?<=\\)[M-Z][^\\]*$	|

Scenario Outline: test me now
    Given the User updates the Extraction Rule with <Name>

	Examples:
	| Name   |
	| foobar |