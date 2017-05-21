namespace EmpGrid.Domain
{
    /// <summary>
    /// Non-generic version to allow Exception constructors to take any identity object as a parameter.
    /// </summary>
    public interface IEntityIdentity
    {
        /// <summary>
        /// The identity formatted in a way that a (possibly technical) human being can understand it.
        /// </summary>
        string HumanReadableId { get; }
    }

    /// <summary>
    /// For any class that can identify an entity.
    /// </summary>
    public interface IEntityIdentity<T> : IEntityIdentity
    {
        /// <summary>
        /// The thing that identifies another entity.
        /// </summary>
        T Id { get; set; }
    }
}
