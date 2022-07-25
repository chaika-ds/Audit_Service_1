using AuditService.Handlers.Consts;
using Nest;

namespace AuditService.Handlers.Extensions;

/// <summary>
///     ELK extension methods
/// </summary>
public static class ElasticSearchExtensions
{
    /// <summary>
    ///     This extension method should only be used in expressions which are analysed by Nest.
    ///     When analysed it will append KEYWORD to the path separating it with a dot.
    ///     This is especially useful with multi fields.
    /// </summary>
    public static object UseSuffix(this object @object) => @object.Suffix(ElasticConst.SuffixKeyword);
}