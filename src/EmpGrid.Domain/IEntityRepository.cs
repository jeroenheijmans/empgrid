namespace EmpGrid.Domain
{
    /// <summary>
    /// Common funcitonality for all aggregate root entity repositories.
    /// </summary>
    public interface IEntityRepository<TEntity, TId, TEntityIdentity> 
        where TEntity : IEntity<TId>, IAggregate
        where TEntityIdentity : IEntityIdentity<TId>
              
    {
        /// <summary>
        /// Looks for an entity by its identity. Returns null if not found.
        /// </summary>
        TEntity FindById(TEntityIdentity identity);

        /// <summary>
        /// Looks for an entity by the id of the identity. Returns null if not found.
        /// </summary>
        TEntity FindById(TId id);

        /// <summary>
        /// Looks for an entity by its identity. Throws an exception if not found.
        /// </summary>
        TEntity GetById(TEntityIdentity identity);

        /// <summary>
        /// Looks for an entity by the id of its identity. Throws an exception if not found.
        /// </summary>
        TEntity GetById(TId id);
    }
}
