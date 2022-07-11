using Microsoft.EntityFrameworkCore;
using NetTest.Domain.Common.Seed;
using NetTest.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetTest.Infrastructure.Common.Repositories
{
    public class AsyncRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class
    {
        private readonly NetTestDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public AsyncRepository(NetTestDbContext context)
        {
            _context = context ?? throw new ArgumentNullException("context");
        }
        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (unitOfWork == null)
                {
                    unitOfWork = new UnitOfWork(this._context);
                }
                return unitOfWork;
            }
            set
            {
                unitOfWork = new UnitOfWork(this._context);
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null || _context == null)
            {
                throw new ArgumentNullException("entity");
            }
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

       

       
        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await DbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task UpdateAsync(TEntity entity)
        {

            var keyName = DbContext.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties
                .Select(x => x.Name).Single();
            var keyValue = entity.GetType().GetProperty(keyName).GetValue(entity, null);

            var attachedObject = DbContext.ChangeTracker
                .Entries().FirstOrDefault(x => x.Metadata.FindPrimaryKey().Properties.First(y => y.Name == keyName) == keyValue);
            if (attachedObject != null)
            {
                attachedObject.State = EntityState.Detached;
            }


            DbContext.Entry(entity).State = EntityState.Modified;
            DbContext.Set<TEntity>().Update(entity);
            DbContext.SaveChanges();
            }
        private DbContext DbContext
        {
            get
            {
                // if (this._context == null)
                // {
                //     if (this._connectionStringName == string.Empty)
                //         this._context = DbContextManager.Current;
                //     else
                //         this._context = DbContextManager.CurrentFor(this._connectionStringName);
                // }
                return this._context;
            }
        }
        private IUnitOfWork unitOfWork;
    }
}
