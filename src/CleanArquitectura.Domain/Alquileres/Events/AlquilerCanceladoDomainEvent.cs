using CleanArquitectura.Domain.Abstraccions;

namespace CleanArquitectura.Domain.Alquileres.Events;

public sealed record AlquilerCanceladoDomainEvent(AlquilerId AlquilerId) : IDomainEvents;