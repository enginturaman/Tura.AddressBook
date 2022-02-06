using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tura.AddressBook.Domain.Entities
{
    public class PersonalDmo : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Firm { get; set; }

        public IEnumerable<PersonalContactDmo> Contacts { get; set; }
    }
}
