Feature: CropServiceTests
	As a developer
	I want to create a new crop through the API
	In order to make it available for the user

	Background:
		Given the EndPoint https://localhost:7170/api/v1/crops is available

	@create-crop
	Scenario: Create a new crop
		Given there is a company with id 1
		When a Post Request is sent
		Then a Response is received with Status 200
		And the response body contains the phase "Formula"
		And the response body contains the status True