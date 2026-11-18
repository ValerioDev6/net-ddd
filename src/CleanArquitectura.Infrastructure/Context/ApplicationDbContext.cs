using CleanArquitectura.Application.Abstractions.Clock;
using CleanArquitectura.Application.Exceptions;
using CleanArquitectura.Domain.Abstraccions;
using CleanArquitectura.Infrastructure.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CleanArquitectura.Infrastructure.Context;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    private static readonly JsonSerializerSettings jsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    // private readonly IPublisher _publisher;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ApplicationDbContext(DbContextOptions options, IDateTimeProvider dateTimeProvider) : base(options)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            await AddDomainEventsToOutboxMessages();

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyExceptions("la excepcion por concurrencia se disparo", ex);
        }
    }

    private async Task AddDomainEventsToOutboxMessages()
    {
        var outboxMessages = ChangeTracker
            .Entries<IEntity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents();
                entity.ClearDomainEvents();
                return domainEvents;
            })
            .Select(domainEvent => new OutboxMessage(
                Guid.NewGuid(),
                DateTime.UtcNow,
                domainEvent.GetType().Name,
                JsonConvert.SerializeObject(domainEvent, jsonSerializerSettings)
            ))
            .ToList();

        AddRange(outboxMessages);

        // foreach (var domainEvent in outboxMessages)
        // {
        //     await _publisher.Publish(domainEvent);
        // }

    }

}
