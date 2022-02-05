using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tura.AddressBook.Domain.Entities
{
    public class PersonalContactDmo : BaseEntity
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        [ForeignKey("Location")]
        public Guid LocationId { get; set; }

        public LocationDmo Location { get; set; }

        [ForeignKey("Personel")]
        public Guid PersonelId { get; set; }

        public PersonalDmo Personel { get; set; }
    }


}
