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
    private static readonly List<(UsePipelineBehaviorsAttribute UsePipelineAttribute, Type RequestType, Type ResponseType)> ArgumentsThatUsePipelines = GetHandlersArguments();

    /// <summary>
    ///     Get handlers arguments that have a UsePipelineBehaviors attribute
    /// </summary>
    /// <returns>Handler arguments</returns>
    public static List<(UsePipelineBehaviorsAttribute UsePipelineAttribute, Type RequestType, Type ResponseType)> GetArgumentsThatUsePipelines() => ArgumentsThatUsePipelines;

    /// <summary>
    ///     Get handlers arguments that have a UsePipelineBehaviors attribute
    /// </summary>
    /// <returns>Handler arguments</returns>
    private static List<(UsePipelineBehaviorsAttribute UsePipelineAttribute, Type RequestType, Type ResponseType)> GetHandlersArguments() 
        => (
            from type in Assembly.GetExecutingAssembly().GetTypes()
            from interfaceType in type.GetInterfaces()
            let baseType = type.BaseType
            let usePipelineAttribute = type.GetCustomAttribute(typeof(UsePipelineBehaviorsAttribute), false)
            where
                !type.IsAbstract && usePipelineAttribute is UsePipelineBehaviorsAttribute &&
                ((baseType is { IsGenericType: true } &&
                  typeof(IRequestHandler<,>).IsAssignableFrom(baseType.GetGenericTypeDefinition())) ||
                 (interfaceType is { IsGenericType: true } &&
                  typeof(IRequestHandler<,>).IsAssignableFrom(interfaceType.GetGenericTypeDefinition())))
            select (
                UsePipelineBehaviors: usePipelineAttribute as UsePipelineBehaviorsAttribute,
                RequestType: interfaceType.GetGenericArguments()[0],
                ResponseType: interfaceType.GetGenericArguments()[1]
            )
        ).ToList();
}