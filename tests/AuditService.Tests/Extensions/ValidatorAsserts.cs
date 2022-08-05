using FluentValidation.TestHelper;

namespace AuditService.Tests.Extensions;

/// <summary>
/// Validator testing Assert
/// </summary>
/// <typeparam name="T"></typeparam>
internal class ValidatorAsserts<T>
{
    /// <summary>
    /// Assert if models name are in validated exceptions
    /// </summary>
    /// <param name="result">TestValidationResult of validating model</param>
    /// <param name="checkedNames">Param names for check</param>
    internal static void AssertDomainModelNames(TestValidationResult<T>? result, List<string> checkedNames)
    {
        checkedNames.ForEach(name =>
        {
            Contains(name, result!.Errors.FirstOrDefault(err => err.ErrorMessage.Contains(name))!.ErrorMessage);
        });
    }
}