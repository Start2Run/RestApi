using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Common;
using Common.Models;
using Persistence.Extensions;
using Persistence.Managers;
using Persistence.Models;
using PersistenceTests.Helpers;
using Xunit;

namespace PersistenceTests
{
    public class PersistenceTests
    {
        [Fact]
        public async Task Should_add_items_to_db()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());
            var configuration = fixture.Create<IConfigurationModel>();
            var connectionFactory = new InMemoryConnectionFactory();
            var databaseHandler = new DatabaseHandler(connectionFactory, configuration);
            var db = new DatabaseManager(databaseHandler);

            connectionFactory.GetConnection().CreateTableIfNotExists(Globals.TableName);

            await db.Clear();
            var items = new List<DbModel>();
            for (var i = 0; i < 10; i++)
            {
                var model = new DbModel
                {
                    Latitude = 60 + i,
                    Longitude = 60 - i,
                    DateTime = DateTime.Now.ToShortTimeString(),
                    Temperature = i
                };
                items.Add(model);
                await db.Insert(model);
            }
            var result = await db.GetAllData();
            Assert.Equal(items, result);
        }

        [Fact]
        public async Task Should_clear_items_from_db()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());
            var configuration = fixture.Create<IConfigurationModel>();
            var connectionFactory = new InMemoryConnectionFactory();
            var databaseHandler = new DatabaseHandler(connectionFactory, configuration);
            var db = new DatabaseManager(databaseHandler);

            connectionFactory.GetConnection().CreateTableIfNotExists(Globals.TableName);

            await db.Clear();
            for (var i = 0; i < 10; i++)
            {
                await db.Insert(new DbModel
                {
                    Latitude = 60 + i,
                    Longitude = 60 - i,
                    DateTime = DateTime.Now.ToShortTimeString(),
                    Temperature = i
                });
            }
            await db.Clear();
            var result = await db.GetAllData();
            Assert.Empty(result);
        }
    }
}
