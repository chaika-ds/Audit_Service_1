using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditService.Common.Health
{
    public class HealthService : IHealthService, IHealthMarkService
    {
        private readonly Timer _decrementTimer;
        private long _errorsCount;

        public HealthService(IHealthSettings settings)
        {
            _errorsCount = 0;
            _decrementTimer = new Timer(Decrement, null, 0, settings.ForPeriodInSec * 1000);
        }

        public long GetErrorsCount()
        {
            return Interlocked.Read(ref _errorsCount);
        }

        public void MarkError()
        {
            Interlocked.Increment(ref _errorsCount);
        }

        private void Decrement(object state)
        {
            Interlocked.Decrement(ref _errorsCount);
            Interlocked.CompareExchange(ref _errorsCount, 0, -1);
        }
    }
}
