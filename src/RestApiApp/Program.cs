using System;
using Business.Managers;
using Common.Enums;
using Common.Models;
using Communication.Managers;
using Persistence.Managers;

namespace RestApiApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationModel();
            var connectionFactory = new DatabaseConnectionFactory(configuration);
            var dbHandler = new DatabaseHandler(connectionFactory,configuration);
            var dbManager = new DatabaseManager(dbHandler);
            var requestManager = new RequestManager(configuration);
            var scheduler = new SchedulerManager(configuration);
            var mainManager = new MenuManager(requestManager, scheduler, dbManager, configuration);
            configuration.Load();

            if (!dbManager.Init())
            {
                Console.WriteLine("Could not initialise the DB");
            }

            var showMenu = true;
            while (showMenu)
            {
                Console.Clear();
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1) Get temperature from REST API endpoint");
                Console.WriteLine("2) Show stored DB entries");
                Console.WriteLine("3) Clear Database Table data");
                Console.WriteLine("4) Exit");
                Console.Write("\r\nSelect an option: ");

                var option = Console.ReadLine();
                if (!Enum.TryParse(option, out MenuOption menuOption))
                {
                    continue;
                }

                showMenu = mainManager.SelectOption(menuOption);
            }
        }
    }
}
