using AuditService.Common.Enums;

namespace AuditService.ELK.FillTestData.Models;

public class PlayerChangesLogConfigModel: BaseConfig
{
    /// <summary>
    ///     Service ID
    /// </summary>
    public ModuleName? ModuleName { get; set; }
    
    /// <summary>
    ///     Event Initiator
    /// </summary>
    public EventInitiator? EventInitiator { get; set; }
    
}