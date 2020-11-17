using System;
using System.Threading.Tasks;
using ApiConnection.Contracts;
using ApiConnection.Managers;
using Business.Contracts;
using Business.Managers;
using Common.Extensions;
using Common.Models;
using Newtonsoft.Json;

namespace RestApiApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationModel();
            configuration.Load();
            DoWorkAsync(configuration).FireAndForget();
            Console.ReadLine();
        }

        public static async Task DoWorkAsync(ConfigurationModel configuration)
        {
            var requestManager = new RequestManager();
            var scheduler = new SchedulerManager(requestManager, configuration); 
            scheduler.Start();
        }
    }
}
