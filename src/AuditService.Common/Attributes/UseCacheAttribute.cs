namespace AuditService.Common.Attributes
{
    /// <summary>
    /// Request caching attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class UseCacheAttribute : Attribute
    {
        /// <summary>
        /// Cache lifetime in seconds
        /// </summary>
        public int Lifetime { get; set; } = 30;

        /// <summary>
        /// The key to store data in the cache
        /// </summary>
        public string? Key { get; set; }
    }
}
