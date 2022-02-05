using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tura.AddressBook.Domain.Models
{
    public class LocationReportValueModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int PersonalCount { get; set; }
        public int PhoneNumberCount { get; set; }
        public Guid LocationReportId { get; set; }

    }
}
