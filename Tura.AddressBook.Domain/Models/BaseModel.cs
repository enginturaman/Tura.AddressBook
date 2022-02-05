using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tura.AddressBook.Domain.Models
{
    public class BaseModel
    {
        public Guid? Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool Deleted { get; set; }
    }
}
