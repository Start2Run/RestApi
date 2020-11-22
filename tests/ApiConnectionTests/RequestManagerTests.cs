using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Common;
using Common.Models;
using Common.Models.RestApi;
using Communication.Managers;
using Flurl.Http.Testing;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace ApiConnectionTests
{
    public class RequestManagerTests
    {
        [Fact]
        public async Task RequestManager_should_return_the_expected_model()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());
            var configuration = fixture.Create<IConfigurationModel>();
            var requestManager = new RequestManager(configuration);

            Mock.Get(configuration).SetupGet(config => config.ApiAddress).Returns("https://localhost");

            using var httpTest = new HttpTest();
            var expectedJson = JsonConvert.SerializeObject(new Root());
            httpTest.RespondWith(expectedJson);

            var value = await requestManager.GetRequestAsync();
            var receivedJson = JsonConvert.SerializeObject(value.WeatherModel);

            Assert.True(value.IsSuccessful);
            Assert.Equal(expectedJson, receivedJson);
        }

        [Fact]
        public async Task RequestManager_should_return_failure_when_calling_the_incorrect_endpoint()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());
            var configuration = fixture.Create<IConfigurationModel>();
            var requestManager = new RequestManager(configuration);

            var endpointAddress = "https://localhost";
            var apiKey = "apiKey1";
            var apiHost = "apiHost1";
            var longitude = "10.0";
            var latitude = "15.2";

            Mock.Get(configuration).SetupGet(config => config.ApiAddress).Returns(endpointAddress);
            Mock.Get(configuration).SetupGet(config => config.ApiKey).Returns(apiKey);
            Mock.Get(configuration).SetupGet(config => config.ApiHost).Returns(apiHost);
            Mock.Get(configuration).SetupGet(config => config.Longitude).Returns(longitude);
            Mock.Get(configuration).SetupGet(config => config.Latitude).Returns(latitude);

            using var httpTest = new HttpTest();
            await requestManager.GetRequestAsync();

            httpTest.ShouldHaveCalled($"{endpointAddress}?{Globals.Longitude}={longitude}&{Globals.Latitude}={latitude}")
            .WithHeader(Globals.ApiKey, apiKey)
            .WithHeader(Globals.ApiHost, apiHost).Times(1);
        }

        [Fact]
        public async Task RequestManager_should_return_failure_when_the_endpoint_timeout()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());
            var configuration = fixture.Create<IConfigurationModel>();
            var requestManager = new RequestManager(configuration);

            Mock.Get(configuration).SetupGet(config => config.ApiAddress).Returns("https://localhost");

            using var httpTest = new HttpTest();
            httpTest.SimulateTimeout();
            var received = await requestManager.GetRequestAsync();

            Assert.False(received.IsSuccessful);
        }
    }
}
