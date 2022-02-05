using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tura.AddressBook.Domain.Enums;

namespace Tura.AddressBook.Domain.Models
{
    public class LocationReportModel : BaseModel
    {
        public ReportStatus Status { get; set; }
        public IEnumerable<LocationReportValueModel> Values { get; set; }
    }
}
