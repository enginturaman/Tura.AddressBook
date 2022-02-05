using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tura.AddressBook.Domain.Entities;

namespace Tura.AddressBook.Interfaces.Repositories
{
    public interface IReportRepository
    {
        void CreateLocationReport();
        IEnumerable<LocationReportDmo> GetLocationReports();
    }
}
