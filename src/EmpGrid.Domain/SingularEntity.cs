using System;

namespace EmpGrid.Domain
{
    /// <summary>
    /// System-generated entities
    /// </summary>
    public abstract class SingularEntity : IEntity<string>
    {
        /// <summary>
        /// Unique identifier for this entity.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Returns the Id as a string.
        /// </summary>
        public string HumanReadableId => Id.ToString();
    }
}
