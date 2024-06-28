Feature: HomePageTests

Just some basic checks that the expected things appear on the page

@tag1
Scenario: Home page renders as expected
	When I navigate to "/"
	Then The page heading is "Welcome"
