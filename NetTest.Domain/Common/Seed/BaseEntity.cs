using System;

namespace NetTest.Domain.Common.Seed
{
    public abstract class BaseEntity<T> where T : class
    {
        public virtual long Id { get; set; }
        public abstract T MapToModel();
        public abstract T MapToModel(T t);
    }
}
