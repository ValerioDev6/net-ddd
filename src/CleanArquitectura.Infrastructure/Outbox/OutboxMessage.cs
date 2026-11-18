using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArquitectura.Infrastructure.Outbox
{
    public sealed class OutboxMessage
    {
        public OutboxMessage(Guid id, DateTime ocurredOnUtc, string? type, string? content)
        {
            Id = id;
            OcurredOnUtc = ocurredOnUtc;
            Type = type;
            Content = content;

        }

        public Guid Id { get; init; }
        public DateTime OcurredOnUtc { get; init; }
        public string? Type { get; init; }
        public string? Content { get; init; }
        public DateTime? ProccesedOnUtc { get; init; }

        public string? Error { get; init; }

    }
}
