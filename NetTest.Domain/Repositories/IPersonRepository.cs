using NetTest.Domain.Common.Seed;
using NetTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NetTest.Domain.Resposittories
{
    public interface IPersonRepository: IAsyncRepository<Person>
    {
        Task<IEnumerable<Person>> GetAllPersonsFiltered(Expression<Func<Person,bool>> personPredicate);
        Task<Person> GetPersonById(long id);

    }
}
