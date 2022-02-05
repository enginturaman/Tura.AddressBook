using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tura.AddressBook.Domain.Models;

namespace Tura.AddressBook.Services.Interfaces.Services
{
    public interface IPersonalService
    {
        public IEnumerable<PersonalModel> Get();
        PersonalDetailModel GetById(Guid id);
        Task<Guid?> Post(PersonalModel model);
        void Put(Guid id, PersonalModel model);
        void Delete(Guid id);
    }
}


