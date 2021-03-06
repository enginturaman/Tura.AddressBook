using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tura.AddressBook.Domain.Entities;
using Tura.AddressBook.Domain.Models;
using Tura.AddressBook.Infrastructures.Exceptions;
using Tura.AddressBook.Services.Interfaces.Repositories;

namespace Tura.AddressBook.Repositories
{
    public class PersonalRepository : IPersonalRepository
    {
        private readonly AddressBookContext _context;

        public PersonalRepository(AddressBookContext context)
        {
            _context = context;
        }

        public void Delete(Guid id)
        {
            var entity = _context.Personals.FirstOrDefault(x => x.Id == id && !x.Deleted);

            if (entity == null)
            {
                throw new NotFoundException("Kullanıcı bilgisi mevcut degil", "RecordNotFound");
            }

            entity.Deleted = true;

            _context.SaveChanges();
        }

        public IEnumerable<PersonalDmo> Get()
        {
            return _context.Personals.Where(x => !x.Deleted).AsEnumerable();
        }

        public PersonalDmo GetById(Guid id)
        {
            return _context.Personals
                .Include(x => x.Contacts)
                .ThenInclude(x => x.Location)
                .FirstOrDefault(x => x.Id == id && !x.Deleted);
        }

        public async Task<Guid?> Post(PersonalDmo entity)
        {
            var isExists = _context.Personals.Any(x => x.Name == entity.Name && x.SurName == entity.SurName && !x.Deleted);

            if (isExists)
            {
                throw new FoundException("Kullanıcı bilgisi mevcut.");
            }

            await _context.Personals.AddAsync(entity);

            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public void Put(Guid id, PersonalModel model)
        {
            var entity = _context.Personals.FirstOrDefault(x => x.Id == id && !x.Deleted);

            if (entity == null)
            {
                throw new NotFoundException("Kullanıcı bilgisi mevcut degil", "RecordNotFound");
            }

            entity.Name = model.Name;
            entity.SurName = model.SurName;
            entity.Company = model.Company;
            entity.UpdatedDate = DateTime.Now;


            _context.SaveChanges();
        }
    }
}
