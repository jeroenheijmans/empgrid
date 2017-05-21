using System;

namespace EmpGrid.Domain
{
    /// <summary>
    /// Any entity that can be created through user input.
    /// </summary>
    public abstract class BulkEntity : IEntity<Guid>
    {
        /// <summary>
        /// Unique identifier for this entity.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Returns the Id as a string.
        /// </summary>
        public string HumanReadableId => Id.ToString();
    }
}
