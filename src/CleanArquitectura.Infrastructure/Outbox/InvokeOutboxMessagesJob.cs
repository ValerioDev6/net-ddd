using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CleanArquitectura.Application.Abstractions.Clock;
using CleanArquitectura.Application.Data;
using CleanArquitectura.Domain.Abstraccions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;

namespace CleanArquitectura.Infrastructure.Outbox;

[DisallowConcurrentExecution]
internal sealed class InvokeOutboxMessagesJob : IJob
{
    private static readonly JsonSerializerSettings jsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly IPublisher _publisher;

    private readonly IDateTimeProvider _dateTimeProvider;

    private readonly OutboxOptions _outboxOptions;

    private readonly ILogger<InvokeOutboxMessagesJob> _logger;

    public InvokeOutboxMessagesJob(
        ISqlConnectionFactory sqlConnectionFactory,
        IPublisher publisher,
        IDateTimeProvider dateTimeProvider,
        IOptions<OutboxOptions> outboxOptions,
        ILogger<InvokeOutboxMessagesJob> logger
    )
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _publisher = publisher;
        _dateTimeProvider = dateTimeProvider;
        _outboxOptions = outboxOptions.Value;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Iniciando el proceso de outbox messages");

        using var connection = _sqlConnectionFactory.CreateConnection();
        using var transacction = connection.BeginTransaction();

        var sql = """

            SELECT
                id, content
            FROM outbox_messages
            WHERE proccessed_on_utc IS NULL,
            ORRDER BY ocurred_on_utc
            LIMIT {_outboxOptions.BatchSize}
            FOR UPDATE
        """;

        var records = (await connection.QueryAsync<OutboxMessageData>(sql, transacction)).ToList();

        foreach (var message in records)
        {
            Exception? exception = null;
            try
            {

                var domainEvent = JsonConvert.DeserializeObject<IDomainEvents>(
                    message.Content,
                    jsonSerializerSettings
                );

                await _publisher.Publish(domainEvent!, context.CancellationToken);


            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Se produjo una excepcion en el outbox message {MessageId}", message.id
                );
                exception = ex;
            }

            await UpdateOutboxMessage(connection, transacction, message, exception);
        }

        transacction.Commit();
        _logger.LogInformation("Finalizando el proceso de outbox messages");


    }


    public async Task UpdateOutboxMessage(
        IDbConnection connection,
        IDbTransaction transaction,
        OutboxMessageData message,
        Exception? exception
    )
    {

        const string sql = @"
                    UPDATE outbox_messages
                        SET processed_on_utc=@ProccesedOnUtc, error = @Error
                    WHERE id = @Id;
        ";


        await connection.ExecuteAsync(sql,
            new
            {
                message.id,
                ProccesedOnUtc = _dateTimeProvider.currentTime,
                Error = exception?.ToString()
            },
            transaction
        );

    }
}

public record OutboxMessageData(Guid id, string Content);
