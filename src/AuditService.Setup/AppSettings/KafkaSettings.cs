﻿using Microsoft.Extensions.Configuration;
using Tolar.Kafka;
using Tolar.Web.Tools;

namespace AuditService.Setup.AppSettings;

internal class KafkaSettings : IKafkaSettings
{
    public KafkaSettings(IConfiguration configuration)
    {
        Config = SettingsHelper.GetKafkaConfiguration(configuration);
    }

    public string? Topic => null;

    public Dictionary<string, string>? Config { get; }
}