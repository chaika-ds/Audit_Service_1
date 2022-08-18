using System.ComponentModel;

namespace AuditService.Common.Enums;

/// <summary>
///     Type of action
/// </summary>
// ReSharper disable InconsistentNaming
public enum ActionType
{
    /// <summary>
    ///     Action for CREATE
    /// </summary>
    [Description("Действие для создания")] 
    CREATE = 0,

    /// <summary>
    ///     Action for UPDATE
    /// </summary>
    [Description("Действие для обновления")]
    UPDATE = 1,

    /// <summary>
    ///     Action for DELETE
    /// </summary>
    [Description("Действие по удалению")] 
    DELETE = 2,

    /// <summary>
    ///     Action for SOFT_DELETE
    /// </summary>
    [Description("Действие для обратимого удаления")]
    SOFT_DELETE = 3,

    /// <summary>
    ///     Action for EXPORT
    /// </summary>
    [Description("Действие на экспорт")] 
    EXPORT = 4,

    /// <summary>
    ///     Action for SYNC
    /// </summary>
    [Description("Действие для синхронизации")]
    SYNC = 5,

    /// <summary>
    ///     Action for VIEW
    /// </summary>
    [Description("Действие для просмотра")]
    VIEW = 6,
    
    /// <summary>
    ///     Action for START
    /// </summary>  
    [Description("Действие для запуска")] 
    START = 7,

    /// <summary>
    ///     Action for STOP
    /// </summary>
    [Description("Действие для остановки")]
    STOP = 8,

    /// <summary>
    ///     Action for BULK
    /// </summary>
    [Description("Действие для объемoм")] 
    BULK = 9,
    
    /// <summary>
    ///     Action for ENABLE
    /// </summary> 
    [Description("Действие для включения")]
    ENABLE = 10,

    /// <summary>
    ///     Action for DISABLE
    /// </summary> 
    [Description("Действие по отключению")]
    DISABLE = 11
}