﻿using AuditService.Common.Models.Domain.BlockedPlayersLog;
using AuditService.Setup.AppSettings;
using AuditService.Tests.AuditService.GetAuditLog.Models;
using AuditService.Tests.Factories.Fakes;
using AuditService.Tests.Resources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Tests.Fakes;

/// <summary>
/// Fake ServiceCollection class for DI unit test
/// </summary>
internal static class ServiceCollectionFake
{
    /// <summary>
    /// Fake IServiceCollection for unit test
    /// </summary>
    internal static IServiceCollection CreateServiceCollectionFake()
    {
        var serviceCollectionFake = new ServiceCollection();
        serviceCollectionFake.AddScoped<IConfiguration, ConfigurationFake>();

        return serviceCollectionFake;
    }
}
