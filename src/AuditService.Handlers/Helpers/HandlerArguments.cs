using System.Reflection;
using AuditService.Handlers.PipelineBehaviors.Attributes;
using MediatR;

namespace AuditService.Handlers.Helpers;

/// <summary>
///     Class for working with handler arguments
/// </summary>
internal static class HandlerArguments
{
    /// <summary>
    ///     Handlers arguments that have a UsePipelineBehaviors attribute
    /// </summary>
    private static readonly List<(UsePipelineBehaviors UsePipelineAttribute, Type RequestType, Type ResponseType)> ArgumentsThatUsePipelines;

    static HandlerArguments()
    {
        ArgumentsThatUsePipelines = GetHandlersArguments();
    }


    /// <summary>
    ///     Get handlers arguments that have a UsePipelineBehaviors attribute
    /// </summary>
    /// <returns>Handler arguments</returns>
    public static List<(UsePipelineBehaviors UsePipelineAttribute, Type RequestType, Type ResponseType)> GetArgumentsThatUsePipelines() => ArgumentsThatUsePipelines;

    /// <summary>
    ///     Get handlers arguments that have a UsePipelineBehaviors attribute
    /// </summary>
    /// <returns>Handler arguments</returns>
    private static List<(UsePipelineBehaviors UsePipelineAttribute, Type RequestType, Type ResponseType)> GetHandlersArguments() 
        => (
            from type in Assembly.GetExecutingAssembly().GetTypes()
            from interfaceType in type.GetInterfaces()
            let baseType = type.BaseType
            let usePipelineAttribute = type.GetCustomAttribute(typeof(UsePipelineBehaviors), false)
            where
                !type.IsAbstract && usePipelineAttribute is UsePipelineBehaviors &&
                ((baseType is { IsGenericType: true } &&
                  typeof(IRequestHandler<,>).IsAssignableFrom(baseType.GetGenericTypeDefinition())) ||
                 (interfaceType is { IsGenericType: true } &&
                  typeof(IRequestHandler<,>).IsAssignableFrom(interfaceType.GetGenericTypeDefinition())))
            select (
                UsePipelineBehaviors: usePipelineAttribute as UsePipelineBehaviors,
                RequestType: interfaceType.GetGenericArguments()[0],
                ResponseType: interfaceType.GetGenericArguments()[1]
            )
        ).ToList();
}