using Microsoft.EntityFrameworkCore;
using Persistence.Models;

namespace Persistence
{
    public class WeatherInformationContext : DbContext
    {
        public WeatherInformationContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<DbModel> DbModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=WeatherInformation.db");
    }
}