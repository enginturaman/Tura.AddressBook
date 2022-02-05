using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tura.AddressBook.Domain.Entities;
using Tura.AddressBook.Domain.Models;

namespace Tura.AddressBook.Services.Interfaces.Repositories
{

    public interface IPersonalRepository
    {
        Task<Guid?> Post(PersonalDmo entity);
        IEnumerable<PersonalDmo> Get();
        PersonalDmo GetById(Guid id);
        void Put(Guid id, PersonalModel model);
        void Delete(Guid id);
    }
}

