namespace EmpGrid.Domain.Core
{
    /// <summary>
    /// Any kind of social- or traditional medium, e.g. twitter, a blog, a home page, etc.
    /// </summary>
    public class Medium : SingularEntity, IAggregate
    {
        /// <summary>
        /// Constructs a default Medium instance.
        /// </summary>
        public Medium(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        /// <summary>
        /// The common display name for this medium.
        /// </summary>
        public string Name { get; set; }
    }
}
