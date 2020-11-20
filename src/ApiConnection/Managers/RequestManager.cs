using System;
using System.Threading.Tasks;
using Common;
using Common.Models;
using Common.Models.RestApi;
using Communication.Contracts;
using Flurl;
using Flurl.Http;

namespace Communication.Managers
{
    public class RequestManager : IRequestManager
    {
        private readonly IConfigurationModel _configuration;
        public RequestManager(IConfigurationModel configuration)
        {
            _configuration = configuration;
        }

        public async Task<RestApiModel> GetRequestAsync()
        {
            var address = _configuration.ApiAddress;
            var response = new RestApiModel { IsSuccessful = true };
            try
            {
                var result = await address
                    .SetQueryParam(Globals.Longitude, _configuration.Longitude)
                    .SetQueryParam(Globals.Latitude, _configuration.Latitude)
                    .WithHeader(Globals.ApiKey, _configuration.ApiKey)
                    .WithHeader(Globals.ApiHost, _configuration.ApiHost).GetJsonAsync<Root>();

                response.WeatherModel = result;
            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                Console.WriteLine(e);
            }

            return response;
        }
    }
}