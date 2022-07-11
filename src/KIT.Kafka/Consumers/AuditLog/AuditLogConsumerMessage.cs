using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;

namespace KIT.Kafka.Consumers.AuditLog
{
    /// <summary>
    /// Audit log consumer message
    /// </summary>
    public class AuditLogConsumerMessage
    {
        /// <summary>
        ///     Module Name
        /// </summary>
        public ServiceStructure ModuleName { get; set; }
        
        /// <summary>
        ///     ID of the node where the change occurred
        /// </summary>
        public Guid NodeId { get; set; }
        
        /// <summary>
        ///     Node Type
        /// </summary>
        public NodeType NodeType { get; set; }

        /// <summary>
        ///     Node Name
        /// </summary>
        public string NodeName { get; set; }
        
        /// <summary>
        ///     Type of action
        /// </summary>
        public ActionType ActionName { get; set; }
        
        /// <summary>
        ///     Category of actions (depending on modules)
        /// </summary>
        public string CategoryCode { get; set; }

        /// <summary>
        ///     The text representation of the request
        /// </summary>
        public string RequestUrl { get; set; }
        
        /// <summary>
        ///     The JSON representation of the request
        /// </summary>
        public object RequestBody { get; set; }
        
        /// <summary>
        ///     Date and time of the event (ISO 8601 UTC standard)
        /// </summary>
        public DateTime Timestamp { get; set; }
        
        /// <summary>
        ///     The name of the class (or table in the database) of the logged entity
        /// </summary>
        public string EntityName { get; set; }
        
        /// <summary>
        ///     ID of the logged entity (possible types of UUID/Long values)
        /// </summary>
        public string EntityId { get; set; }
        
        /// <summary>
        ///     JSON representation of the previous value of the entity
        /// </summary>
        public string OldValue { get; set; }
        
        /// <summary>
        ///     JSON representation of a new entity value
        /// </summary>
        public string NewValue { get; set; }
        
        /// <summary>
        ///     ID of the project from which the change is logged in the audit
        /// </summary>
        public Guid ProjectId { get; set; }
        
        /// <summary>
        ///     User
        /// </summary>
        public IdentityUserDomainModel User { get; set; }
    }
}
