using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tura.AddressBook.Domain.Enums;

namespace Tura.AddressBook.Domain.Entities
{
 
    public class LocationReportDmo : BaseEntity
    {
        public ReportStatus Status { get; set; }

        public IEnumerable<LocationReportValueDmo> Values { get; set; }
    }
}
