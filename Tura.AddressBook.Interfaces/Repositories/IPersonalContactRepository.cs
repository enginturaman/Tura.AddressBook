using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tura.AddressBook.Domain.Entities;
using Tura.AddressBook.Domain.Models;

namespace Tura.AddressBook.Services.Interfaces.Repositories
{
    public interface IPersonalContactRepository
    {
        Guid? Post(PersonalContactModel model);
        IEnumerable<PersonalContactDmo> Get();
        PersonalContactDmo GetById(Guid id);
        void Put(Guid id, PersonalContactModel model);
        void Delete(Guid id);
    }
}

