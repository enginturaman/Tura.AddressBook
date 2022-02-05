using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tura.AddressBook.Domain.Entities
{
    public class LocationDmo : BaseEntity
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}
