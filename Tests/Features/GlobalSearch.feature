Feature: Global search
As an EPAM website user  
I want to perform a search on the main page     

Background:
	Given I navigate to the EPAM website
	And I click on Accept Cookies button

Scenario Outline: Global search results contain the search term
	When I click on the Search icon element
	And I enter the text '<searchTerm>' into the search input
	And I click on the Find button
	Then All the elements of the search results contain '<searchTerm>'

Examples:
	| searchTerm |
	| BLOCKCHAIN |
	| RPA        |
