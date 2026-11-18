using CleanArquitectura.Domain.Abstraccions;

namespace CleanArquitectura.Domain.Users.Events;

public sealed record UserCreatedDomainEvent(UserId UserId): IDomainEvents;