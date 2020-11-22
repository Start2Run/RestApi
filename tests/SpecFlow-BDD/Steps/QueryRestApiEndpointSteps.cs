using System;
using TechTalk.SpecFlow;

namespace SpecFlowBDD.Steps
{
    [Binding]
    public class QueryRestApiEndpointSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public QueryRestApiEndpointSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"a valid endpoint address is configured in the application configuration file")]
        public void GivenAValidEndpointAddressIsConfiguredInTheApplicationConfigurationFile()
        {
            _scenarioContext.Pending();
        }
        
        [When(@"the request is sent to a Rest Api endpoint")]
        public void WhenTheRequestIsSentToTheRestApiEndpoint()
        {
            _scenarioContext.Pending();
        }
        
        [Then(@"the returned body data should have the format JSON data model")]
        public void ThenTheReturnedBodyDataShouldHaveTheFormatJSONDataModel()
        {
            _scenarioContext.Pending();
        }
    }
}
