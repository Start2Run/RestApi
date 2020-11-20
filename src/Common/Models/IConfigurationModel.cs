namespace Common.Models
{
    public interface IConfigurationModel
    {
        string DatabaseName { get; }
        int PullIntervalInSeconds { get; }
        string ApiAddress { get; }
        string ApiKey { get; }
        string ApiHost { get; }

        string Longitude { get; }
        string Latitude { get; }
    }
}