using AuditService.Common.Services;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuditService.EventConsumerApp
{
    public class InputServicesManager : IHostedService
    {
        private readonly IEnumerable<IInputService> _inputs;

        public InputServicesManager(IEnumerable<IInputService> inputs)
        {
            _inputs = inputs;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (var x in _inputs)
            {
                x.Start();
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (var x in _inputs)
            {
                x.Stop();
            }

            return Task.CompletedTask;
        }
    }
}
