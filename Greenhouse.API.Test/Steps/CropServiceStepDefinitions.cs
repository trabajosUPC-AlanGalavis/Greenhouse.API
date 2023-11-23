using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Greenhouse.API.Crops.Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using Xunit;

namespace Greenhouse.API.Test.Steps;

[Binding]
public sealed class CropsServiceStepDefinitions :
    WebApplicationFactory<Program>
{
    private readonly WebApplicationFactory<Program> _factory;

    public CropsServiceStepDefinitions(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    private HttpClient Client { get; set; }
    private Uri BaseUri { get; set; }

    private Task<HttpResponseMessage> Response { get; set; }
    
    [Given(@"the EndPoint https://localhost:(.*)/api/v(.*)/crops is available")]
    public void GivenTheEndPointHttpsLocalhostApiVCropsIsAvailable(int p0, int p1)
    {
        BaseUri = new Uri($"https://localhost:{p0}/api/v{p1}/crops/");
    }

    [Given(@"there is a company with id (.*)")]
    public void GivenThereIsACompanyWithId(int p0)
    {
        //get BaseUri and append the id
        Console.WriteLine(BaseUri);
        BaseUri = new Uri($"{BaseUri}{p0}");
        Client = _factory.CreateClient(new WebApplicationFactoryClientOptions {BaseAddress = BaseUri});
    }

    [When(@"a Post Request is sent")]
    public void WhenAPostRequestIsSent()
    {
        Response = Client.PostAsync(BaseUri, null);
    }

    [Then(@"a Response is received with Status (.*)")]
    public void ThenAResponseIsReceivedWithStatus(int expectedStatus)
    {
        var expectedStatusCode = ((HttpStatusCode)expectedStatus).ToString();
        var actualStatusCode = Response.Result.StatusCode.ToString();
        
        Assert.Equal(expectedStatusCode, actualStatusCode);
    }


    [Then(@"the response body contains the phase ""(.*)""")]
    public async Task ThenTheResponseBodyContainsThePhase(string formula)
    {
        var responseData = await Response.Result.Content.ReadAsStringAsync();
        var resource = JsonConvert.DeserializeObject<TestCrop>(responseData);
        Assert.Equal(formula, resource.Phase);
    }

    [Then(@"the response body contains the status True")]
    public void ThenTheResponseBodyContainsTheStatusTrue()
    {
        var responseData = Response.Result.Content.ReadAsStringAsync().Result;
        var resource = JsonConvert.DeserializeObject<TestCrop>(responseData);
        Assert.True(resource.State);
    }
}

public class TestCrop
{
    public string Phase { get; set; }
    public bool State { get; set; }
    // Exclude StartDate and EndDate for testing purposes
}