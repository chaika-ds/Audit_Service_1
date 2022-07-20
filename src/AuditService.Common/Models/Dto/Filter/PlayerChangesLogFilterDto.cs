namespace AuditService.Common.Models.Dto.Filter
{
    /// <summary>
    /// Filter model for player card changelog
    /// </summary>
    public class PlayerChangesLogFilterDto
    {
        public PlayerChangesLogFilterDto()
        {
            EventKeys = new List<string>();
        }

        /// <summary>
        ///     User login
        /// </summary>
        public string? Login { get; set; }

        /// <summary>
        ///     IP address of the user making the change
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        ///     Start date
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        ///     End date
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        ///     Language for localization
        /// </summary>
        public string? Language { get; set; }

        /// <summary>
        /// Event keys
        /// </summary>
        public IEnumerable<string> EventKeys { get; set; }
    }
}
