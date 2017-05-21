namespace EmpGrid.Domain.Core
{
    /// <summary>
    /// Determines who is allowed to view other items.
    /// </summary>
    public enum Visibility
    {
        /// <summary>
        /// Anonymous read access.
        /// </summary>
        Public = 0,

        /// <summary>
        /// Only logged in / organizational people are allowed to see this.
        /// </summary>
        Organization = 1,

        /// <summary>
        /// Not publicized, only the emp can see this.
        /// </summary>
        Private = 2,
    }
}
