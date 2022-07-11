using LinqKit;
using NetTest.Domain.Common.Services;
using NetTest.Domain.Entities;
using NetTest.Domain.InterFaces;
using NetTest.Domain.Models;
using NetTest.Domain.Resposittories;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetTest.Domain.Services
{
    public class PersonService : CRUD<PersonEntity, Person>, IPersonService
    {
        private readonly IPersonRepository _personRepository;
       public PersonService(IPersonRepository personRepository):base(personRepository)
        {
            _personRepository = personRepository;
        }
        public async Task<IEnumerable<Person>> GetAllFiltered(string firstName, string lastName, string city)
        {
            var filterPerson = PredicateBuilder.New<Person>();
            bool flag = false;
            if (!string.IsNullOrEmpty(firstName))
            {
                filterPerson = filterPerson.And(p => p.FirstName.ToLower().Contains(firstName.ToLower()));
                flag = true;
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                filterPerson = filterPerson.And(p => p.LastName.ToLower().Contains(lastName.ToLower()));
                flag = true;
            }
            if (!string.IsNullOrEmpty(city))
            {
                filterPerson = filterPerson.And(p => p.Address.City.ToLower().Contains(city.ToLower()));
                flag = true;
            }
            filterPerson=flag?filterPerson:null;
            try
            {
                return await _personRepository.GetAllPersonsFiltered(filterPerson);
            }
            catch
            {
                return null;
            }
        }

        public async Task<long> UpdatePerson(PersonEntity person)
        {
            var existPerson = await _personRepository.GetPersonById(person.Id);
            var personModel = person.MapToModel(existPerson);
            await _personRepository.UpdateAsync(personModel);
            return person.Id;
        }
    }
}
