using AuditService.Common.Kafka;
using AuditService.Test.EventProducer.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AuditService.IntegrationTests.Settings
{
    internal class AppSettings1 : IKafkaSettings, IDirectorSettings
    {
        private static readonly Lazy<AppSettings1> lazy = new Lazy<AppSettings1>(() => new AppSettings1(), LazyThreadSafetyMode.PublicationOnly);
        public string GroupId { get; set; }

        public string Address { get; set; }

        public string Topic { get; set; }

        public Dictionary<string, string> Config { get; set; }

        public Dictionary<string, string> Topics { get; set; }

        public AppSettings1 GetInstance(IConfiguration configuration)
        {
            GroupId = configuration["Kafka:GroupId"];
            Address = configuration["Kafka:Address"];
            Config = configuration.GetSection("Kafka:Config").GetChildren().ToDictionary(x => x.Key, v => v.Value);

            Topics = configuration.GetSection("KafkaTopics").GetChildren().ToDictionary(x => x.Key, v => v.Value);
            return lazy.Value;
        }

        private AppSettings1()
        {
            var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder();

            // Add services to the container.

            builder.Services.AddServices();

            var app = builder.Build();

            IWebHostEnvironment env = app.Environment;

            builder.Configuration.AddJsonFile("appsettings.json");
            builder.Configuration.AddJsonFile($"appsettings.{env}.json", optional: true);
            builder.Configuration.AddEnvironmentVariables();

            app.Run();
        }        
    }
}
