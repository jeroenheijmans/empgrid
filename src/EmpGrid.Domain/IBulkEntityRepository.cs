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
        PutResult Put(T entity);
        void Delete(IEntityIdentity<Guid> identity);
        void Delete(Guid id);
        IQueryable<T> Query();
    }
}
