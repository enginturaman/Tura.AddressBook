using System;
using System.Collections.Generic;
using Tura.AddressBook.Domain.Entities;
using Tura.AddressBook.Domain.Models;

namespace Tura.AddressBook.Services.Interfaces.Services
{
    public interface IPersonalContactService
    {
        public IEnumerable<PersonalContactModel> Get();
        PersonalContactModel GetById(Guid id);
        Guid? Post(PersonalContactModel model);
        void Put(Guid id, PersonalContactModel model);
        void Delete(Guid id);
    }
}


