namespace EmpGrid.Domain.Core
{
    /// <summary>
    /// Online presence, e.g. a blog or social medium.
    /// </summary>
    public class Presence : BulkEntity
    {
        /// <summary>
        /// Main url to the <see cref="Emp"/>'s presence.
        /// </summary>
        public string Url { get; set; }
        
        /// <summary>
        /// The medium associated with this presence.
        /// </summary>
        public string MediumId { get; set; }

        /// <summary>
        /// Who is allowed to see this presence.
        /// </summary>
        public Visibility Visibility { get; set; }
    }
}
