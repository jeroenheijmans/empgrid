using System;
using System.Linq;

namespace EmpGrid.Domain
{
    /// <summary>
    /// Functionality for user-generated-entity repositories.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBulkEntityRepository<T> 
        : IEntityRepository<T, Guid, IEntityIdentity<Guid>> 
        where T : BulkEntity, IAggregate
    {
        void Put(T entity);
        void Put(params T[] entities);
        IQueryable<T> Query();
    }
}
