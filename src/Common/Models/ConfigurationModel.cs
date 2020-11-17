
using System.Configuration;

namespace Common.Models
{
    public class ConfigurationModel
    {
        public int PullIntervalInSeconds { get; private set; }
        public string ApiAddress { get; private set; }
        public string ApiKey { get; private set; }
        public string ApiHost { get; private set; }

        public string Longitude { get; private set; }
        public string Latitude { get; private set; }


        public void Load()
        {
            PullIntervalInSeconds = int.TryParse(ConfigurationManager.AppSettings.Get(Globals.PullIntervalInSeconds), out var interval) ? interval : Globals.DefaultPullIntervalInSeconds;
            ApiAddress = ConfigurationManager.AppSettings.Get(Globals.ApiAddress);
            ApiKey = ConfigurationManager.AppSettings.Get(Globals.ApiKey);
            ApiHost = ConfigurationManager.AppSettings.Get(Globals.ApiHost);
            Longitude = ConfigurationManager.AppSettings.Get(Globals.Longitude);
            Latitude = ConfigurationManager.AppSettings.Get(Globals.Latitude);
        }
    }
}