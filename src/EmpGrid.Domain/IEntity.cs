namespace EmpGrid.Domain
{
    /// <summary>
    /// Interface for all Domain entities. An entity class is obviously an identity.
    /// </summary>
    public interface IEntity<T> : IEntityIdentity<T>
    {
    }
}

