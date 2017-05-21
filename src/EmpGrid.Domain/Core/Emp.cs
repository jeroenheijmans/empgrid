using System.Collections.Generic;

namespace EmpGrid.Domain.Core
{
    /// <summary>
    /// Any person that wants to be included in the grid
    /// </summary>
    public class Emp : BulkEntity, IAggregate
    {
        /// <summary>
        /// The display name for this emp. Free-form, though first names or full names are recommended options.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Where this emp can be reached.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// A short description of this emp.
        /// </summary>
        public string TagLine { get; set; }

        /// <summary>
        /// The mediums where the emp can be found.
        /// </summary>
        public ICollection<Presence> Presences { get; private set; } = new List<Presence>();
    }
}
