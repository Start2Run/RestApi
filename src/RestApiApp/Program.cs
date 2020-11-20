using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Business.Managers;
using Common;
using Common.Models;
using Communication.Managers;
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
            var dbHandler = new DatabaseHandler(configuration);
            var dbManager = new DatabaseManager(dbHandler);

            configuration.Load();

            if (!dbManager.Init())
            {
                Console.WriteLine("Could not initialise the DB");
            }

            var showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu(configuration, dbManager);
            }
        }

        private static bool MainMenu(IConfigurationModel configuration, IDbManager dbManager)
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Get temperature from REST API endpoint");
            Console.WriteLine("2) Show stored DB entries");
            Console.WriteLine("3) Clear Database Table data");
            Console.WriteLine("4) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    ReadApiData(configuration, dbManager);
                    return true;
                case "2":
                    ReadDbEntries(dbManager).GetAwaiter();
                    return true;
                case "3":
                    ClearTable(dbManager).GetAwaiter();
                    return true;
                default:
                    return false;
            }
        }


        private static void ReadApiData(IConfigurationModel configuration, IDbManager dbManager)
        {
            Console.WriteLine($"Wait {configuration.PullIntervalInSeconds} seconds to receive the data!!!");
            var requestManager = new RequestManager(configuration);
            var scheduler = new SchedulerManager(configuration);
            var mainManager = new MainManager(requestManager, scheduler, dbManager);
            mainManager.Init();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static async Task ReadDbEntries(IDbManager dbManager)
        {
            var entries = await dbManager.GetAllData();
            foreach (var item in entries)
            {
                Console.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented));
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static async Task ClearTable(IDbManager dbManager)
        {
            await dbManager.Clear();
            Console.WriteLine($"Database table {Globals.TableName} has been cleared. Press any key to continue...");
            Console.ReadKey();
        }
    }
}
