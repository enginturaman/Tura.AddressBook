using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tura.AddressBook.Domain.Entities
{
    public class LocationReportValueDmo 
    {
        [Key]
        public Guid Id { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public int PersonalCount { get; set; }
        public int PhoneNumberCount { get; set; }

        [ForeignKey("LocationReport")]
        public Guid LocationReportId { get; set; }

        public LocationReportDmo LocationReport { get; set; }
    }
}
