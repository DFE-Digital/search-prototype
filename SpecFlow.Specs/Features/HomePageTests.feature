Feature: HomePageTests

Just some basic checks that the expected things appear on the page

@tag1
Scenario: Home page renders as expected
	When I navigate to the home page
	Then The page heading is "Welcome"

Scenario: Home page has link to Privacy page
	When I am on the home page
	Then I can locate the Privacy link in the header
	And The Privacy link takes me to the privacy page
