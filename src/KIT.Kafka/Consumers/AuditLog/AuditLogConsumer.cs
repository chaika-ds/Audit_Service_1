using System.Text;
using AuditService.Common.Consts;
using AuditService.Common.Enums;
using AuditService.Common.Extensions;
using FluentValidation;
using FluentValidation.Results;
using KIT.Kafka.Consumers.Base;
using KIT.Kafka.Settings.Interfaces;
using KIT.NLog.Extensions;
using KIT.RocketChat.Commands.PostBufferedTextMessage;
using KIT.RocketChat.Commands.PostBufferedTextMessage.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using static System.Enum;

namespace KIT.Kafka.Consumers.AuditLog;

/// <summary>
///     Audit log consumer
/// </summary>
public class AuditLogConsumer : BaseConsumer<AuditLogConsumerMessage>
{
    private readonly IPostBufferedMessageCommand _postBufferedMessageCommand;
    private readonly IValidator<AuditLogConsumerMessage> _validator;
    private readonly ITopicValidationSettings _topicValidationSettings;
    private readonly ILogger<AuditLogConsumer> _logger;

    public AuditLogConsumer(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _validator = serviceProvider.GetRequiredService<IValidator<AuditLogConsumerMessage>>();
        _postBufferedMessageCommand = serviceProvider.GetRequiredService<IPostBufferedMessageCommand>();
        _topicValidationSettings = serviceProvider.GetRequiredService<ITopicValidationSettings>();
        _logger = serviceProvider.GetRequiredService<ILogger<AuditLogConsumer>>();
    }

    /// <summary>
    ///     Consumer method for receiving/listening to messages
    /// </summary>
    /// <param name="context">The context of consumption</param>
    /// <returns>Task execution result</returns>
    protected override async Task Consume(ConsumeContext<AuditLogConsumerMessage> context)
    {
        try
        {
            if (context.Message is null)
            {
                await PostBufferedErrorMessageAsync(context.OriginalContext.Data, GetModuleNameFromMessage(context.OriginalContext.Data));
                return;
            }

            if (await _validator.ValidateAsync(context.Message) is var validationResult && validationResult.IsValid)
                return;

            await PostBufferedErrorMessageAsync(context.OriginalContext.Data, context.Message.ModuleName, validationResult.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogException(ex, "AuditLogConsumer operation failed", context.OriginalContext);
        }
    }

    /// <summary>
    ///     Get the topic within which the consumer will listen to messages
    /// </summary>
    /// <param name="kafkaTopics">Topics kafka</param>
    /// <returns>Topic for listening to messages</returns>
    protected override string GetTopic(IKafkaTopics kafkaTopics) => kafkaTopics.AuditLog;

    /// <summary>
    ///     Post buffered validation error message
    /// </summary>
    /// <param name="topicMessage">Topic message</param>
    /// <param name="moduleName">Identificator of service</param>
    /// <param name="errors">List of validation errors</param>
    /// <returns>Execution result</returns>
    private async Task PostBufferedErrorMessageAsync(string topicMessage, ModuleName? moduleName,
        List<ValidationFailure>? errors = null)
    {
        var errorMessage = BuildErrorMessage(topicMessage, moduleName, errors);
        var bufferKey = CreateBufferKey(moduleName);
        await _postBufferedMessageCommand.Execute(new PostBufferedMessageRequest(_topicValidationSettings.ValidationResultChat!, errorMessage, bufferKey));
    }

    /// <summary>
    ///     Get module name from message(json message from topic)
    /// </summary>
    /// <param name="topicMessage">Topic message</param>
    /// <returns>Module name(Identificator of service)</returns>
    private ModuleName? GetModuleNameFromMessage(string topicMessage)
    {
        try
        {
            var data = JObject.Parse(topicMessage);
            var moduleNameStringValue = data[nameof(AuditLogConsumerMessage.ModuleName).ToCamelCase()]?.Value<string>();

            if (string.IsNullOrEmpty(moduleNameStringValue))
                return null;

            return Parse<ModuleName>(moduleNameStringValue);
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    ///     Create a buffering key to post message
    /// </summary>
    /// <param name="moduleName">Identificator of service</param>
    /// <returns>Buffer key</returns>
    private string CreateBufferKey(ModuleName? moduleName) => $"{Name}_{moduleName}";

    /// <summary>
    ///     Build a message about a message validation error from a topic
    /// </summary>
    /// <param name="topicMessage">Topic message</param>
    /// <param name="moduleName">Identificator of service</param>
    /// <param name="errors">List of validation errors</param>
    /// <returns>Error message</returns>
    private string BuildErrorMessage(string topicMessage, ModuleName? moduleName, List<ValidationFailure>? errors)
    {
        var responsibleService = moduleName is not null ? moduleName.Value.Description() : ModuleConst.All;

        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("*Auditlog validation failed!*");
        stringBuilder.AppendLine($"*Topic:*  {KafkaTopics.AuditLog}");
        stringBuilder.AppendLine($"*Responsible service:* {responsibleService}");
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
}