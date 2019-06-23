﻿Feature: SpecFlowFeature1
	In order to make a Restful Client Request and get a Response
	As a user
	I want to be able to provide an end point and get a response

Scenario: Confirm that a request and response can be generated
	Given I enter the http://api.zippopotam.us site
	When I enter the postcode /us/90210
	Then the response code is: 200