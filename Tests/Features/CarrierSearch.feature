@UI
Feature: Carrier search
As an EPAM website user  
I want to perform a search on the carriers page    

Background:
	Given I navigate to the EPAM website
	And I click on Accept Cookies button

Scenario Outline: Carrier search result contains the search term
	Given I navigate to Carriers page
	When I enter the text '<language>' into the search field
	And I set the location to '<location>' '<city>'
	And I chose remote
	And I click on Find button
	And I sort the results by date
	And I click on the last Apply button
	Then The '<language>' is on the resulting page
	
Examples:
	| language | location      | city   |
	| C        | All Locations |        |
	| Java     | Colombia      | Bogota |

