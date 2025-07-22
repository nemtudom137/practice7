using Business.API.ApplicationInterface;
using Business.API.ApplicationInterface.Models;
using Core;
using Core.API;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;

namespace Business.API.Steps
{
    [Binding]
    public class JsonPlaceholderSteps
    {
        private readonly ScenarioContext scenarioContext;
        private readonly UsersClient usersClient;
        private readonly InvalidEndpointClient invalidClient;

        public JsonPlaceholderSteps(IApiClient client, IRequestBuilder builder, ScenarioContext scenarioContext)
        {
            usersClient = new UsersClient(client, builder);
            invalidClient = new InvalidEndpointClient(client, builder);
            this.scenarioContext = scenarioContext;
        }

        [When(@"I send request to /users GET method")]
        public async Task WhenISendRequestToUsersGETMethod()
        {
            var response = await usersClient.GetUsersAsync();
            LogHelper.Log.Info("Request sent to /users GET");
            scenarioContext.Set(response, "response");
        }

        [Then(@"I get a list of users")]
        public void ThenIGetAListOfUsers()
        {
            var response = scenarioContext.Get<RestResponse<List<User>>>("response");
            var isListOfUsers = response.Data?.All(x => x.IsValid()) ?? false;
            LogHelper.Log.Info($"The list of users in the response is{(isListOfUsers ? " " : " not ")}valid");

            Assert.That(isListOfUsers, Is.True);
        }

        [Then(@"The response code is (.*)")]
        public void ThenTheResponseCodeIsOK(int code)
        {
            var response = scenarioContext.Get<RestResponse>("response");
            var statusCode = (int)response.StatusCode;
            LogHelper.Log.Info($"Response status code: {statusCode}");

            Assert.That(statusCode, Is.EqualTo(code));
        }

        [Then(@"There are no error messages")]
        public void ThenThereAreNoErrorMessages()
        {
            var response = scenarioContext.Get<RestResponse>("response");
            LogHelper.Log.Info($"Response error message: {response.ErrorMessage ?? "empty"}");

            Assert.That(response.ErrorMessage, Is.Null);
        }

        [Then(@"I content-type header exists in the obtained response")]
        public void ThenIContent_TypeHeaderExistsInTheObtainedResponse()
        {
            var response = scenarioContext.Get<RestResponse>("response");
            var containsType = response.ContentHeaders?.Any(x => x.Name == "Content-Type") ?? false;
            LogHelper.Log.Info($"Header {(containsType ? "contains" : "does no contain")} Content-Type");

            Assert.That(containsType, Is.True);
        }

        [Then(@"The value of the content-type header is (.*)")]
        public void ThenTheValueOfTheContent_TypeHeaderIsApplicationJsonCharsetUtf_(string value)
        {
            var response = scenarioContext.Get<RestResponse>("response");
            var parameter = response.ContentHeaders?.FirstOrDefault(x => x.Name == "Content-Type");
            LogHelper.Log.Info($"Content-Type value: {parameter?.Value}");

            Assert.That(parameter?.Value, Is.EqualTo(value));
        }

        [Then(@"The content of the response body is the array of (.*) users")]
        public void ThenTheContentOfTheResponseBodyIsTheArrayOfUsers(int n)
        {
            var response = scenarioContext.Get<RestResponse<List<User>>>("response");
            int count = response.Data?.Count ?? 0;
            LogHelper.Log.Info($"The response contains {count} users");

            Assert.That(count, Is.EqualTo(n));
        }

        [Then(@"Each user has a unique ID")]
        public void ThenEachUserHasAUniqueID()
        {
            var response = scenarioContext.Get<RestResponse<List<User>>>("response");
            var uniqueIds = response.Data?.DistinctBy(x => x.Id).Count();
            LogHelper.Log.Info($"The response contains {response.Data?.Count} users and {uniqueIds} of them has unique ID");

            Assert.That(response.Data?.Count, Is.EqualTo(uniqueIds));
        }

        [Then(@"Each user has non-empty Name and Username")]
        public void ThenEachUserHasNon_EmptyNameAndUsername()
        {
            var response = scenarioContext.Get<RestResponse<List<User>>>("response");
            var allUsersHasNameUsername = response.Data?.All(x => !string.IsNullOrEmpty(x.Name) && !string.IsNullOrEmpty(x.UserName)) ?? false;
            LogHelper.Log.Info($"{(allUsersHasNameUsername ? "All" : "Not all")} the users has Name and Username fields");

            Assert.That(allUsersHasNameUsername, Is.True);
        }

        [Then(@"Each user contains the Company with non-empty Name")]
        public void ThenEachUserContainsTheCompanyWithNon_EmptyName()
        {
            var response = scenarioContext.Get<RestResponse<List<User>>>("response");
            var allUsershasCompany = response.Data?.All(x => !string.IsNullOrEmpty(x.Company?.Name)) ?? false;
            LogHelper.Log.Info($"{(allUsershasCompany ? "All" : "Not all")} the users has company field");

            Assert.That(allUsershasCompany, Is.True);
        }

        [When(@"I send request to /users POST method with Name '([^']*)' and Username '([^']*)' fields")]
        public async Task WhenISendRequestToUsersPOSTMethodWithNameAndUsernameFields(string name, string username)
        {
            var user = new User { Name = name, UserName = username };
            var response = await usersClient.CreateUserAsync(user);
            LogHelper.Log.Info($"Request sent to /users POST with values {name} {username}");
            scenarioContext.Set(response, "response");
        }

        [Then(@"The response is not empty")]
        public void ThenTheResponseIsNotEmpty()
        {
            var response = scenarioContext.Get<RestResponse>("response");
            LogHelper.Log.Info($"Respons {(string.IsNullOrEmpty(response.Content) ? "empty" : "not empty")}");

            Assert.That(response.Content, Is.Not.Null.Or.Empty);
        }

        [Then(@"The response contains an ID value")]
        public void ThenTheResponseContainsAnIDValue()
        {
            var response = scenarioContext.Get<RestResponse<User>>("response");
            LogHelper.Log.Info($"ID in the response: {response.Data?.Id}");

            Assert.That(response.Data?.Id > 0, Is.True);
        }

        [When(@"I send request to /invalidendpoint GET method")]
        public async Task WhenISendRequestToInvalidendpointGETMethod()
        {
            var response = await invalidClient.GetAsync();
            LogHelper.Log.Info("Request sent to /invalidendpoint GET");
            scenarioContext.Set(response, "response");
        }
    }
}
