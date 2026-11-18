using CleanArquitectura.Domain.Abstraccions;

namespace CleanArquitectura.Domain.Alquileres.Events;

public sealed record AlquilerRechazadoDomainEvent(AlquilerId Id) : IDomainEvents;