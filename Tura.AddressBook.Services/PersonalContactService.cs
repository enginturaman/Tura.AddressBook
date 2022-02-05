using System;
using System.Collections.Generic;
using System.Linq;
using Tura.AddressBook.Domain.Entities;
using Tura.AddressBook.Domain.Models;
using Tura.AddressBook.Infrastructures.Exceptions;
using Tura.AddressBook.Services.Interfaces.Repositories;
using Tura.AddressBook.Services.Interfaces.Services;

namespace Tura.AddressBook.Services
{
    public class PersonalContactService : IPersonalContactService
    {
        private readonly IPersonalContactRepository _personalContactRepository;

        public PersonalContactService(IPersonalContactRepository personalContactRepository)
        {
            _personalContactRepository = personalContactRepository;
        }

        public void Delete(Guid id)
        {
            _personalContactRepository.Delete(id);
        }

        public IEnumerable<PersonalContactModel> Get()
        {
            return _personalContactRepository.Get()
                .Select(x => new PersonalContactModel
                {
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Id = x.Id,
                    PersonalId = x.PersonelId,
                    Location = new LocationModel
                    {
                        Lat = x.Location.Lat,
                        Lon = x.Location.Lon,
                        Id = x.Location.Id
                    }
                });
        }

        public PersonalContactModel GetById(Guid id)
        {
            var entity = _personalContactRepository.GetById(id);

            if (entity == null)
            {
                throw new NotFoundException("Personal İletişim Kaydı Bulunamadı", "RecordNotFound");
            }

            return new PersonalContactModel
            {
                Id = entity.Id,
                Email = entity.Email,
                PersonalId = entity.PersonelId,
                PhoneNumber = entity.PhoneNumber,
                Location = new LocationModel
                {
                    Id = entity.Location.Id,
                    Lat = entity.Location.Lat,
                    Lon = entity.Location.Lon
                }
            };
        }

        public Guid? Post(PersonalContactModel model)
        {

           return  _personalContactRepository.Post(model);
        }

        public void Put(Guid id, PersonalContactModel model)
        {
            _personalContactRepository.Put(id ,model);
        }
    }
}
