@UI
Feature: Services section navigation
As an EPAM website user  
I want to see the articles about services  

Background:
	Given I navigate to the EPAM website
	And I click on Accept Cookies button

Scenario Outline: Services dropdown redirect to the correct article
	When I hover over Services link in the main navigation
	And I click on the service category '<category>'
	Then I should see the article about '<category>'
	And The section '<section>' is displayed on the page

Examples:
	| category       | section               |
	| Generative AI  | Our Related Expertise |
	| Responsible AI | Our Related Expertise |

