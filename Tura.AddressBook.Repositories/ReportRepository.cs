using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tura.AddressBook.Domain.Entities;
using Tura.AddressBook.Domain.Enums;
using Tura.AddressBook.Domain.Models;
using Tura.AddressBook.Infrastructures.Exceptions;
using Tura.AddressBook.Interfaces.Repositories;

namespace Tura.AddressBook.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly AddressBookContext _context;

        public ReportRepository(AddressBookContext context)
        {
            _context = context;
        }

        public void CreateLocationReport()
        {
            var entity = new LocationReportDmo
            {
                CreatedDate = DateTime.Now,
                Deleted = false,
                Id = Guid.NewGuid(),
                Status = ReportStatus.Preparing
            };

            _context.LocationReports.Add(entity);

            _context.SaveChanges();

            var values = _context.PersonalContacts
                       .Include(x => x.Location)
                       .Include(x => x.Personel)
                       .GroupBy(x => new
                       {
                           Longitude =  x.Location.Lon,
                           Latitude = x.Location.Lat,

                       }).Select(s => new LocationReportValueDmo
                       {
                           Id = Guid.NewGuid(),
                           Lon = s.Key.Longitude,
                           Lat = s.Key.Latitude,
                           PhoneNumberCount = s.Select(x => x.PhoneNumber).Distinct().Count(),
                           PersonalCount = s.Select(x => x.PersonelId).Distinct().Count(),
                           LocationReportId = entity.Id
                       }).ToList();

            _context.LocationReportValues.AddRange(values);

            entity.Status = ReportStatus.Completed;

            _context.SaveChanges();


        }

        public IEnumerable<LocationReportDmo> GetLocationReports()
        {
            return _context.LocationReports
                 .Include(x => x.Values)
                 .Where(x => !x.Deleted);
        }
    }
}
