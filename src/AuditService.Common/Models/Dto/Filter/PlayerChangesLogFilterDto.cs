using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AuditService.Common.Models.Dto.Filter
{
    /// <summary>
    /// Filter model for player card changelog
    /// </summary>
    public class PlayerChangesLogFilterDto : ILogFilter
    {
        public PlayerChangesLogFilterDto()
        {
            EventKeys = new List<string>();
        }

        /// <summary>
        ///     The Id of the player by whom the changes were made
        /// </summary>
        [Required]
        public Guid PlayerId { get; set; }

        /// <summary>
        ///     IP address of the user making the change
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// Event keys
        /// </summary>
        public IEnumerable<string> EventKeys { get; set; }

        /// <summary>
        ///     Start date
        /// </summary>
        [Required]
        [ModelBinder(Name = "startDate")]
        public DateTime TimestampFrom { get; set; }

        /// <summary>
        ///     End date
        /// </summary>
        [Required]
        [ModelBinder(Name = "endDate")]
        public DateTime TimestampTo { get; set; }
    }
}
