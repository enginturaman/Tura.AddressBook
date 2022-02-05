using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tura.AddressBook.Domain.Models;
using Tura.AddressBook.Interfaces.Repositories;
using Tura.AddressBook.Services.Interfaces.Services;

namespace Tura.AddressBook.Services
{
    public class ReportService : IReportService
    {
        private readonly IRabbitMQApiRepository _rabbitMQApiRepository;
        private readonly IReportRepository _reportRepository;

        public ReportService(IRabbitMQApiRepository rabbitMQApiRepository,
            IReportRepository reportRepository)
        {
            _rabbitMQApiRepository = rabbitMQApiRepository;
            _reportRepository = reportRepository;
        }

        public void CreateLocationReport()
        {
            _reportRepository.CreateLocationReport();
        }

        public IEnumerable<LocationReportModel> GetLocationReports()
        {
            var entities = _reportRepository.GetLocationReports().ToList();

            return entities.Select(x => new LocationReportModel
            {
                CreatedDate = x.CreatedDate,
                Deleted = x.Deleted,
                Id = x.Id,
                Status = x.Status,
                UpdatedDate = x.UpdatedDate,
                Values = x.Values.Select(x => new LocationReportValueModel
                {
                    Latitude = x.Lat,
                    LocationReportId = x.LocationReportId,
                    Longitude = x.Lon,
                    PersonalCount = x.PersonalCount,
                    PhoneNumberCount = x.PhoneNumberCount,
                })
            });

        }

        public void PushLocationReport()
        {
            _rabbitMQApiRepository.SendLocationReport();
        }
    }
}
