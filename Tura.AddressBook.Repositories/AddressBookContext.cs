using Microsoft.EntityFrameworkCore;
using Tura.AddressBook.Domain.Entities;

namespace Tura.AddressBook.Repositories
{
    public class AddressBookContext : DbContext
    {
        public AddressBookContext(DbContextOptions<AddressBookContext> options) : base(options)
        {

        }

        public DbSet<PersonalDmo> Personals { get; set; }
        public DbSet<PersonalContactDmo> PersonalContacts { get; set; }
        public DbSet<LocationDmo> Locations { get; set; }
        public DbSet<LocationReportDmo> LocationReports { get; set; }
        public DbSet<LocationReportValueDmo> LocationReportValues { get; set; }
    }
}
