﻿Feature: HomePageTests

Just some basic checks that the expected things appear on the page

@tag1
Scenario: Home page renders as expected
	When I navigate to the home page
	Then The page heading is "Welcome"

#Scenario: Home page has link to Privacy page
#	Given I am on the home page
#	When I click on the Privacy link in the header
#	Then I am taken to the privacy page
