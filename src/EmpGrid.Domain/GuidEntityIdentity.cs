using System;

namespace EmpGrid.Domain
{
    /// <summary>
    /// Lightweith identity for a bulk entity.
    /// </summary>
    public class GuidEntityIdentity : IEntityIdentity<Guid>
    {
        /// <inheritDoc />
        public Guid Id { get; set; }

        /// <inheritDoc />
        public string HumanReadableId => Id.ToString();

        /// <summary>
        /// Converts a string to a Guid-based identity.
        /// </summary>
        public static implicit operator GuidEntityIdentity(string guidString)
        {
            return new GuidEntityIdentity { Id = Guid.Parse(guidString) };
        }

        /// <summary>
        /// Converts a Guid to a Guid-based identity.
        /// </summary>
        public static implicit operator GuidEntityIdentity(Guid guid)
        {
            return new GuidEntityIdentity { Id = guid };
        }
    }
}
