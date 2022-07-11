using NetTest.Domain.Common.Interface.Service;
using NetTest.Domain.Entities;
using NetTest.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetTest.Domain.InterFaces
{
    public interface IPersonService:ICRUD<PersonEntity,Person>
    {
        Task<IEnumerable<Person>> GetAllFiltered(string firstName, string lastName,string city);
        Task<long> UpdatePerson(PersonEntity person);

    }
}
