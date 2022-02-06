using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tura.AddressBook.Domain.Entities;
using Tura.AddressBook.Domain.Models;
using Tura.AddressBook.Infrastructures.Exceptions;
using Tura.AddressBook.Services.Interfaces.Repositories;
using Tura.AddressBook.Services.Interfaces.Services;

namespace Tura.AddressBook.Services
{
    public class PersonalService : IPersonalService
    {
        private readonly IPersonalRepository _personalRepository;

        public PersonalService(IPersonalRepository personalRepository)
        {
            _personalRepository = personalRepository;
        }

        public void Delete(Guid id)
        {
            _personalRepository.Delete(id);
        }

        public IEnumerable<PersonalModel> Get()
        {
            return _personalRepository.Get()
                .Select(x => new PersonalModel
                {
                    Name = x.Name,
                    Firm = x.Firm,
                    Id = x.Id,
                    LastName = x.LastName,
                });
        }

        public PersonalDetailModel GetById(Guid id)
        {
            var entity = _personalRepository.GetById(id);

            if (entity == null)
            {
                throw new NotFoundException("Personal Kaydı Bulunamadı", "RecordNotFound");
            }

            return new PersonalDetailModel
            {
                Name = entity.Name,
                Firm = entity.Firm,
                Id = entity.Id,
                LastName = entity.LastName,
                Contacts = entity.Contacts.Select(t => new PersonalContactModel
                {
                    Id = t.Id,
                    Email = t.Email,
                    PersonalId = t.PersonelId,
                    PhoneNumber = t.PhoneNumber,
                    Location = new LocationModel
                    {
                        Id = t.Location.Id,
                        Lat = t.Location.Lat,
                        Lon = t.Location.Lon
                    }
                })
            };
        }


        public Task<Guid?> Post(PersonalModel model)
        {
            var entity = new PersonalDmo
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                LastName = model.LastName,
                Firm = model.Firm,
                Deleted = false,
                CreatedDate = DateTime.Now,
            };

            var result = _personalRepository.Post(entity);

            return result;
        }

        public void Put(Guid id, PersonalModel model)
        {
            _personalRepository.Put(id, model);
        }

    }
}
