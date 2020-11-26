using CityInfoTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace CityInfoTracker.Persistence
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext() : base()
        {
        }

        public DbSet<CityInfo> CitiesInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=CityInfo.db");
        }
    }
}