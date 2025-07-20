@API
Feature: JSONPlaceholder
As a JSONPlaceholder API  
I want to use its resources  

Scenario: GET metod on '/users' gives a list of users
	When I send request to /users GET method
	Then I get a list of users
	And The response code is 200
	And There are no error messages

Scenario: GET metod on '/users' gives a response with correct header
	When I send request to /users GET method
	Then I content-type header exists in the obtained response
	And The value of the content-type header is application/json; charset=utf-8
	And The response code is 200
	And There are no error messages

Scenario: GET metod on '/users' gives a list of users with correct content
	When I send request to /users GET method
	Then The content of the response body is the array of 10 users
	And Each user has a unique ID
	And Each user has non-empty Name and Username
	And Each user contains the Company with non-empty Name
	And The response code is 200
	And There are no error messages

Scenario Outline: With POST metod on '/users' a user  can be created
	When I send request to /users POST method with Name 'name' and Username 'username' fields
	Then The response is not empty
	And The response contains an ID value
	And The response code is 201
	And There are no error messages
Examples:
	| name | username |
	| name | user1    |

Scenario: Non-existent resource sends correct notification
	When I send request to /invalidendpoint GET method
	Then The response code is 404
	And There are no error messages


