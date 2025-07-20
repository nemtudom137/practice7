@UI
Feature: Carousel navigation
As an EPAM website user  
I want to see the articles presented on carousels   

Background:
	Given I navigate to the EPAM website
	And I click on Accept Cookies button

Scenario Outline: Carousel on Insights page redirect to the correct article
	Given I'm on the Insights page
	When I swipe <times> the topmost carousel
	And I note the name of the article
	And I click on the Read More button
	Then The article title should be the one noted earlier

Examples:
	| times |
	| 0     |
	| 2     |
	| 7     |
	