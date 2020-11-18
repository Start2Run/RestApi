using System;
using ApiConnection.Managers;
using Business.Managers;
using Common.Models;
using Newtonsoft.Json;
using Persistence.Contracts;
using Persistence.Managers;

namespace RestApiApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationModel();
            var dbManager = new DbManager();

            configuration.Load();

            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu(configuration, dbManager);
            }
        }

        private static bool MainMenu(ConfigurationModel configuration, IDbManager dbManager)
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Get temperature from REST API endpoint");
            Console.WriteLine("2) Show stored DB entries");
            Console.WriteLine("3) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    ReadApiData(configuration, dbManager);
                    return true;
                case "2":
                    ReadDbEntries(dbManager);
                    return true;
                case "3":
                    return false;
                default:
                    return true;
            }
        }


        private static void ReadApiData(ConfigurationModel configuration, IDbManager dbManager)
        {
            var requestManager = new RequestManager();
            var scheduler = new SchedulerManager(configuration);
            var mainManager = new MainManager(requestManager, scheduler, dbManager, configuration);
            mainManager.Init();
        }

        private static void ReadDbEntries(IDbManager dbManager)
        {
            var entries = dbManager.GetAllData();
            foreach (var item in entries)
            {
                Console.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented));
            }
        }
    }
}
