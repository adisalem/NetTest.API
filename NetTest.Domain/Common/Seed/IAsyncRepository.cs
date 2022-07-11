using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetTest.Domain.Common.Seed
{
    public interface IAsyncRepository<T> where T : class
    {
        IUnitOfWork UnitOfWork { get; }
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
    }

}
