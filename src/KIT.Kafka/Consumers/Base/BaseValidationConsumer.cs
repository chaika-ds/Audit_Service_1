using System.Text;
using AuditService.Common.Consts;
using AuditService.Common.Enums;
using AuditService.Common.Extensions;
using FluentValidation;
using FluentValidation.Results;
using KIT.Kafka.Consumers.AuditLog;
using KIT.Kafka.Settings.Interfaces;
using KIT.NLog.Extensions;
using KIT.RocketChat.Commands.PostBufferedTextMessage;
using KIT.RocketChat.Commands.PostBufferedTextMessage.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace KIT.Kafka.Consumers.Base;

/// <summary>
///     Base consumer for topic validation
/// </summary>
/// <typeparam name="TModel">Message model type</typeparam>
public abstract class BaseValidationConsumer<TModel> : BaseConsumer<TModel> where TModel : class, new()
{
    private readonly IPostBufferedMessageCommand _postBufferedMessageCommand;
    private readonly IValidator<TModel>? _validator;
    private readonly ITopicValidationSettings _topicValidationSettings;
    private readonly ILogger<TModel> _logger;

    protected BaseValidationConsumer(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _validator = serviceProvider.GetService<IValidator<TModel>>();
        _postBufferedMessageCommand = serviceProvider.GetRequiredService<IPostBufferedMessageCommand>();
        _topicValidationSettings = serviceProvider.GetRequiredService<ITopicValidationSettings>();
        _logger = serviceProvider.GetRequiredService<ILogger<TModel>>();
    }

    /// <summary>
    ///     Get responsible service name
    /// </summary>
    /// <param name="context">The context of consumption</param>
    /// <returns>Responsible service name</returns>
    protected abstract string? GetResponsibleServiceName(ConsumeContext<TModel> context);

    /// <summary>
    ///     Get validated entity name
    /// </summary>
    /// <returns>Validated entity name</returns>
    protected abstract string GetValidatedEntityName();

    /// <summary>
    ///     Consumer method for receiving/listening to messages
    /// </summary>
    /// <param name="context">The context of consumption</param>
    /// <returns>Task execution result</returns>
    protected override async Task ConsumeAsync(ConsumeContext<TModel> context)
    {
        try
        {
            var responsibleServiceName = GetResponsibleServiceName(context);

            if (context.Message is null)
            {
                await PostBufferedErrorMessageAsync(context.OriginalContext.Data, responsibleServiceName);
                return;
            }

            if (_validator is null)
                return;

            if (await _validator.ValidateAsync(context.Message) is var validationResult && validationResult.IsValid)
                return;

            await PostBufferedErrorMessageAsync(context.OriginalContext.Data, responsibleServiceName, validationResult.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogException(ex, $"{Name} operation failed", context.OriginalContext);
        }
    }

    /// <summary>
    ///     Create a buffering key to post message
    /// </summary>
    /// <param name="responsibleServiceName">Responsible service name</param>
    /// <returns>Buffer key</returns>
    protected virtual string CreateBufferKey(string? responsibleServiceName) => $"{Name}_{responsibleServiceName}";

    /// <summary>
    ///     Build a message about a message validation error from a topic
    /// </summary>
    /// <param name="topicMessage">Topic message</param>
    /// <param name="responsibleServiceName">Responsible service name</param>
    /// <param name="errors">List of validation errors</param>
    /// <returns>Error message</returns>
    protected virtual string BuildErrorMessage(string topicMessage, string? responsibleServiceName, List<ValidationFailure>? errors)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"*{GetValidatedEntityName()} validation failed!*");
        stringBuilder.AppendLine($"*Topic:*  {GetTopic(KafkaTopics)}");
        stringBuilder.AppendLine($"*Responsible service:* {responsibleServiceName ?? ModuleConst.All}");
        stringBuilder.AppendLine("*Topic message:*");
        stringBuilder.AppendLine("```json");
        stringBuilder.AppendLine(topicMessage);
        stringBuilder.AppendLine("```");

        if (errors == null || !errors.Any())
            return stringBuilder.ToString();

        stringBuilder.AppendLine("*Validation errors:*");
        errors.ForEach(error => stringBuilder.AppendLine($"> {error.ErrorMessage}"));
        return stringBuilder.ToString();
    }

    /// <summary>
    ///     Get module name from message(json message from topic)
    /// </summary>
    /// <param name="topicMessage">Topic message</param>
    /// <returns>Module name(Identificator of service)</returns>
    protected ModuleName? GetModuleNameFromMessage(string topicMessage)
    {
        try
        {
            var data = JObject.Parse(topicMessage);
            var moduleNameStringValue = data[nameof(AuditLogConsumerMessage.ModuleName).ToCamelCase()]?.Value<string>();

            if (string.IsNullOrEmpty(moduleNameStringValue))
                return null;

            return Enum.Parse<ModuleName>(moduleNameStringValue);
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    ///     Post buffered validation error message
    /// </summary>
    /// <param name="topicMessage">Topic message</param>
    /// <param name="responsibleServiceName">Responsible service name</param>
    /// <param name="errors">List of validation errors</param>
    /// <returns>Execution result</returns>
    private async Task PostBufferedErrorMessageAsync(string topicMessage, string? responsibleServiceName, List<ValidationFailure>? errors = null)
    {
        var errorMessage = BuildErrorMessage(topicMessage, responsibleServiceName, errors);
        var bufferKey = CreateBufferKey(responsibleServiceName);
        await _postBufferedMessageCommand.Execute(new PostBufferedMessageRequest(_topicValidationSettings.ValidationResultChat!, errorMessage, bufferKey));
    }
}