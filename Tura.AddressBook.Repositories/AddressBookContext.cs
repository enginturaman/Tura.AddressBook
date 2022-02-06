using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Tura.AddressBook.Domain.Entities;

namespace Tura.AddressBook.Repositories
{
    public class AddressBookContext : DbContext
    {
        public AddressBookContext() : base()
        {

        }
        public AddressBookContext(DbContextOptions<AddressBookContext> options) : base(options)
        {

        }

        public DbSet<PersonalDmo> Personals { get; set; }
        public DbSet<PersonalContactDmo> PersonalContacts { get; set; }
        public DbSet<LocationDmo> Locations { get; set; }
        public DbSet<LocationReportDmo> LocationReports { get; set; }
        public DbSet<LocationReportValueDmo> LocationReportValues { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory().Replace(".Repositories.Core", ""))
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("AddressBookDbConnection");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }
    }
}
