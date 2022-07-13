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
    ///     Action for CONFIRM
    /// </summary>
    [Description("CONFIRM ACTION")] 
    CONFIRM = 5,

    /// <summary>
    ///     Action for DENY
    /// </summary>
    [Description("Действие для отказа")] 
    DENY = 6,

    /// <summary>
    ///     Action for BIND
    /// </summary>
    [Description("Действие для привязки")] 
    BIND = 7,

    /// <summary>
    ///     Action for UNBIND
    /// </summary>
    [Description("Действие для отмены привязки")]
    UNBIND = 8,

    /// <summary>
    ///     Action for SYNC
    /// </summary>
    [Description("Действие для синхронизации")]
    SYNC = 9,

    /// <summary>
    ///     Action for VERIFICATION
    /// </summary>
    [Description("Действие для проверки")] 
    VERIFICATION = 10,

    /// <summary>
    ///     Action for LOGIN
    /// </summary>
    [Description("Действие для входа")] 
    LOGIN = 11,

    /// <summary>
    ///     Action for LOGOUT
    /// </summary>
    [Description("Действие для выхода")] 
    LOGOUT = 12,

    /// <summary>
    ///     Action for VIEW
    /// </summary>
    [Description("Действие для просмотра")]
    VIEW = 13,

    /// <summary>
    ///     Action for BULK
    /// </summary>
    [Description("Действие для объемoм")] 
    BULK = 14,

    /// <summary>
    ///     Action for START
    /// </summary>  
    [Description("Действие для запуска")] 
    START = 15,

    /// <summary>
    ///     Action for STOP
    /// </summary>
    [Description("Действие для остановки")]
    STOP = 16,

    /// <summary>
    ///     Action for ENABLE
    /// </summary> 
    [Description("Действие для включения")]
    ENABLE = 17,

    /// <summary>
    ///     Action for DISABLE
    /// </summary> 
    [Description("Действие по отключению")]
    DISABLE = 18
}