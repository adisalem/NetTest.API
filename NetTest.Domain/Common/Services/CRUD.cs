using NetTest.Domain.Common.Interface.Service;
using NetTest.Domain.Common.Seed;
using NetTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace NetTest.Domain.Common.Services
{
    public class CRUD<T1, T2> : ICRUD<T1, T2> where T1 : BaseEntity<T2> where T2 : class
    {
        private readonly IAsyncRepository<T2> _repository;

        public CRUD(IAsyncRepository<T2> repository)
        {
            _repository = repository; 
        }


        public async Task<long> Add(T1 t)
        {
            try
            {
               var data= await _repository.AddAsync(t.MapToModel());

                return Convert.ToInt64(data.GetType().GetProperty("Id").GetValue(data,null));
            }
            catch (Exception e)
            {
                return new long();
            }
        }
        public async Task<long> Update(T1 t)
        {
            try
            {
                await this._repository.UpdateAsync(t.MapToModel());
                return new long();
            }
            catch (Exception e)
            {
                return new long();
            }
        }
    }
}
