using NetTest.Domain.Common.Seed;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetTest.Domain.Common.Interface.Service
{
    public interface ICRUD<T1, T2> where T1 : BaseEntity<T2> where T2 : class
    {
        Task<long> Add(T1 t);
        Task<long> Update(T1 t);
    }
}
