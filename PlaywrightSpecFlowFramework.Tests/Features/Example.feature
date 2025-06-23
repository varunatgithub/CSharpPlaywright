Feature: Example Website Interaction
	As a user
	I want to interact with the Playwright website
	So that I can verify its basic functionality

Scenario: Navigate to Playwright website and check title
	Given I am on the Playwright homepage
	When I get the title of the page
	Then the title should contain "Playwright"
	And the title should be "Fast and reliable end-to-end testing for modern web apps | Playwright"
