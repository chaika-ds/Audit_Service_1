﻿using System;
using System.IO;
using AuditService.Common.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

namespace AuditService.EventConsumer;

public class AdditionalEnvironmentConfiguration
{
    /// <summary>
    ///     Adds the JSON configuration provider at <paramref name="pathFile"/> to <paramref name="builder"/>.
    /// </summary>
    /// <remarks>
    ///     Supported docker container directory
    /// </remarks>
    public void AddJsonFile(WebApplicationBuilder builder, string pathFile)
    {
        if (builder.Environment.ContentRootPath == "/app/")
        {
            builder.Configuration.AddJsonFile(pathFile, true, true);
            return;
        }

        var directoryInfo = new DirectoryInfo(builder.Environment.ContentRootPath);
        var configPath = GetParent(directoryInfo)?.FullName;
        if (string.IsNullOrEmpty(configPath))
        {
            Console.WriteLine($"additional config folder in all parts of path '{directoryInfo.FullName}' - not founded!");
            return;
        }

        var fileProvider = new PhysicalFileProvider(configPath);
        builder.Configuration.AddJsonFile(fileProvider, pathFile, true, true);
    }

    /// <summary>
    ///     Find parent root with name from value
    /// </summary>
    private DirectoryInfo? GetParent(DirectoryInfo? directoryInfo)
    {
        while (true)
        {
            if (directoryInfo == null || !directoryInfo.FullName.Contains("src"))
                return directoryInfo;

            directoryInfo = directoryInfo?.Parent;
        }
    }

    public void AddCustomerLogger(WebApplicationBuilder builder, string environmentName)
    {
        builder.Logging.ClearProviders();
        builder.Logging.SetMinimumLevel(LogLevel.Trace);
        builder.Logging.AddAuditServiceLogger(options => {
            builder.Configuration.Bind(options);
            options.Channel = LogChannelParsing.CheckAndParseChannel(environmentName.ToLower());
        });
    }
}