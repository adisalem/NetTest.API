using LinqKit;
using Microsoft.EntityFrameworkCore;
using NetTest.Domain.Entities;
using NetTest.Domain.Models;
using NetTest.Domain.Resposittories;
using NetTest.Infrastructure.Common.Repositories;
using NetTest.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetTest.Infrastructure.Repositories
{
  public  class PersonRepository: AsyncRepository<Person>, IPersonRepository
    {
        private readonly NetTestDbContext _context;

        public PersonRepository(NetTestDbContext context):base(context)
        {
            _context = context;
        }



        public async Task<IEnumerable<Person>> GetAllPersonsFiltered(Expression<Func<Person, bool>> personPredicate)
        {
            if (personPredicate == null)
            {
                return await _context.Persons
                                .Include(a => a.Address)
                                .ToListAsync();
            }

           return await _context.Persons
                                .Include(a => a.Address)
                                .Where(personPredicate).ToListAsync();   
        }

        public async Task<Person> GetPersonById(long id)
        {
            try
            {
                return await _context.Persons
                                   .Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
