using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tura.AddressBook.Domain.Models
{
    public   class LocationModel : BaseModel
    { 

        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}
