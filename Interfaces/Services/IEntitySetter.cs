﻿using System.Linq.Expressions;

namespace Jobber.Services.Interfaces
{
    public interface IEntitySetter<T>
    {
        Task Remove(Expression<Func<T, bool>> predicate);
        Task RemoveRange(Expression<Func<T, bool>> predicate);
        Task Add(T newEntity);
        Task Update(T newEntity);
        Task UpdateOrAdd(T newEntity);
    }
}
