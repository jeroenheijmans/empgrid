namespace EmpGrid.Domain.Core
{
    /// <summary>
    /// Any kind of social- or traditional medium, e.g. twitter, a blog, a home page, etc.
    /// </summary>
    public class Medium : SingularEntity
    {
        /// <summary>
        /// The common display name for this medium.
        /// </summary>
        public string Name { get; set; }
    }
}
