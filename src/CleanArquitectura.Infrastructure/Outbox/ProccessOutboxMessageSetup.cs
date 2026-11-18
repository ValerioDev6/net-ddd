using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Quartz;

namespace CleanArquitectura.Infrastructure.Outbox
{
    public class ProccessOutboxMessageSetup : IConfigureOptions<QuartzOptions>
    {
        // InvokeOutboxMessagesJob
        private readonly OutboxOptions _outboxOptions;

        public ProccessOutboxMessageSetup(IOptions<OutboxOptions> outboxOptions)
        {
            _outboxOptions = outboxOptions.Value;
        }

        public void Configure(QuartzOptions options)
        {
            const string JobName = nameof(InvokeOutboxMessagesJob);

            options
                .AddJob<InvokeOutboxMessagesJob>(configure => configure.WithIdentity(JobName))
                .AddTrigger(configure =>
                    configure
                        .ForJob(JobName)
                        .WithSimpleSchedule(shedule =>
                            shedule.WithIntervalInSeconds(_outboxOptions.IntervalInSeconds)
                            .RepeatForever()
                        )

                );

        }
    }
}
