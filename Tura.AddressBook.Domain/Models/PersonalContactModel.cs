using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tura.AddressBook.Domain.Models
{
    public class PersonalContactModel : BaseModel
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public LocationModel Location { get; set; }

        public Guid PersonalId { get; set; }
    }
 
}
