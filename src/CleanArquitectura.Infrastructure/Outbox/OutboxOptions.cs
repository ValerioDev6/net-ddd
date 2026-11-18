using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArquitectura.Infrastructure.Outbox
{
    public class OutboxOptions
    {
        public int IntervalInSeconds { get; init; }
        public int BatchSize { get; init; }

    }
}
