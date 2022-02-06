using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tura.AddressBook.Domain.Models
{
    public class PersonalModel : BaseModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Firm { get; set; }

    }

    public class PersonalDetailModel : PersonalModel    {

        public IEnumerable<PersonalContactModel> Contacts { get; set; }
    }
}
