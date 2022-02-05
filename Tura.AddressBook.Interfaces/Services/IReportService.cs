using System;
using System.Collections.Generic;
using Tura.AddressBook.Domain.Models;

namespace Tura.AddressBook.Services.Interfaces.Services
{
    public interface IReportService
    {
        void PushLocationReport();
        IEnumerable<LocationReportModel> GetLocationReports();

        void CreateLocationReport();
    }
}


