using System.Collections.Generic;

namespace EmpGrid.Domain.Core
{
    /// <summary>
    /// A collection of emps.
    /// </summary>
    public class Grid
    {
        /// <summary>
        /// The emps contained in this Grid.
        /// </summary>
        public ICollection<Emp> Emps { get; private set; } = new List<Emp>();
    }
}
