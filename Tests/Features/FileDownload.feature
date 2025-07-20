@UI
@download
Feature: File download
As an EPAM website user  
I want to download some files    

Background:
	Given I navigate to the EPAM website
	And I click on Accept Cookies button
	
Scenario Outline: On About page the download button gives the file with correct name
	Given I'm on the About page
	And I scroll down to the EPAM at a Glance section
	When I click on the Download button
	Then A file named '<fileName>' is downloaded

Examples:
	| fileName                              |
	| EPAM_Corporate_Overview_Q4FY-2024.pdf |