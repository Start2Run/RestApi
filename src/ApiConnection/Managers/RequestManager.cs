using System;
using System.Threading.Tasks;
using ApiConnection.Contracts;
using Common;
using Common.Models;
using Common.Models.RestApi;
using Flurl;
using Flurl.Http;

namespace ApiConnection.Managers
{
    public class RequestManager : IRequestManager
    {
        public async Task<Root> GetRequestAsync(ConfigurationModel configuration)
        {
            var address = configuration.ApiAddress;
            
            try
            {
                var result = await address
                    .SetQueryParam(Globals.Longitude, configuration.Longitude)
                    .SetQueryParam(Globals.Latitude, configuration.Latitude)
                    .WithHeader(Globals.ApiKey, configuration.ApiKey)
                    .WithHeader(Globals.ApiHost, configuration.ApiHost)
                    .GetJsonAsync<Root>();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
           
            return null;
        }
    }
}