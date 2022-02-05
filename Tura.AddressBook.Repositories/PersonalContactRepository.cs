using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tura.AddressBook.Domain.Entities;
using Tura.AddressBook.Domain.Models;
using Tura.AddressBook.Infrastructures.Exceptions;
using Tura.AddressBook.Services.Interfaces.Repositories;

namespace Tura.AddressBook.Repositories
{
    public class PersonalContactRepository : IPersonalContactRepository
    {
        private readonly AddressBookContext _context;

        public PersonalContactRepository(AddressBookContext context)
        {
            _context = context;
        }

        public void Delete(Guid id)
        {
            var entity = _context.PersonalContacts.FirstOrDefault(x => x.Id == id && !x.Deleted);

            if (entity == null)
            {
                throw new NotFoundException("Kullanıcı bilgisi mevcut degil", "RecordNotFound");
            }

            entity.Deleted = true;

            _context.SaveChanges();

        }

        public IEnumerable<PersonalContactDmo> Get()
        {
            return _context.PersonalContacts
                .Include(x => x.Location)
                .Where(x => !x.Deleted);
        }

        public PersonalContactDmo GetById(Guid id)
        {
            return _context.PersonalContacts
               .Include(x => x.Location)
               .FirstOrDefault(x => !x.Deleted && x.Id == id);
        }

        public Guid? Post(PersonalContactModel model)
        {
            var locationEntity = _context.Locations.FirstOrDefault(x => x.Lat == model.Location.Lat && x.Lon == model.Location.Lon && !x.Deleted);

            bool isExists = false;

            if (locationEntity == null)
            {
                locationEntity = new LocationDmo
                {
                    CreatedDate = DateTime.Now,
                    Deleted = false,
                    Lat = model.Location.Lat,
                    Lon = model.Location.Lon,
                    Id = Guid.NewGuid()
                };
                _context.Locations.Add(locationEntity);
                _context.SaveChanges();
            }
            else
            {
                isExists = _context.PersonalContacts
                            .Include(x => x.Location)
                            .Any(x => x.Location.Lat == model.Location.Lat
                                                              && x.Location.Lon == model.Location.Lon
                                                              && x.PersonelId == model.PersonalId
                                                              && x.PhoneNumber == model.PhoneNumber
                                                              && !x.Deleted);
            }

            if (isExists)
            {
                throw new FoundException("Konum bilgisi bu kişiye daha önce tanımlanmış.", "RecordIsFound");
            }

            var entity = new PersonalContactDmo
            {
                PersonelId = model.PersonalId,
                CreatedDate = DateTime.Now,
                Deleted = false,
                Email = model.Email,
                Id = Guid.NewGuid(),
                LocationId = locationEntity.Id,
                PhoneNumber = model.PhoneNumber

            };

            _context.PersonalContacts.Add(entity);

            _context.SaveChanges();

            return entity.Id;

        }

        public void Put(Guid id, PersonalContactModel model)
        {
            var entity = _context.PersonalContacts
                .Include(_x => _x.Location)
                .FirstOrDefault(x => x.Id == id && !x.Deleted);

            if (entity == null)
            {
                throw new NotFoundException("Kullanıcı İletişim bilgisi mevcut degil", "RecordNotFound");
            }

            entity.PhoneNumber = model.PhoneNumber;
            entity.Email = model.Email;

            if (model.Location != null)
            {
                if (entity.Location != null)
                {
                    entity.Location.Lon = model.Location.Lon;
                    entity.Location.Lat = model.Location.Lat;
                }
                else
                {
                    entity.Location = new LocationDmo
                    {
                        Lon = model.Location.Lon,
                        Lat = model.Location.Lat
                    };
                }
            }
            entity.UpdatedDate = DateTime.Now;
            _context.SaveChanges();
        }
    }
}
