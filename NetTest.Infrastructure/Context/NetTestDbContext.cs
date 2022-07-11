using Microsoft.EntityFrameworkCore;
using NetTest.Domain.Models;

namespace NetTest.Infrastructure.Context
{
    public  class NetTestDbContext:DbContext
    {
        public NetTestDbContext(DbContextOptions<NetTestDbContext> options):base(options)
        {

        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
