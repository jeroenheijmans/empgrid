namespace EmpGrid.Domain
{
    /// <summary>
    /// A light-weight identity for a singular entity.
    /// </summary>
    public class StringEntityIdentity : IEntityIdentity<string>
    {
        /// <inheritDoc />
        public string Id { get; set; }

        /// <inheritDoc />
        public string HumanReadableId => Id;

        /// <summary>
        /// Converts a string to a String-based identity.
        /// </summary>
        public static implicit operator StringEntityIdentity(string id)
        {
            return new StringEntityIdentity { Id = id };
        }
    }
}
