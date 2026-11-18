using CleanArquitectura.Domain.Abstraccions;

namespace CleanArquitectura.Domain.Reviews.Events;

public sealed record ReviewCreatedDomainEvent(ReviewId ReviewId): IDomainEvents;