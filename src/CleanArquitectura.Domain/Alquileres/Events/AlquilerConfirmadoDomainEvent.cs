using CleanArquitectura.Domain.Abstraccions;

namespace CleanArquitectura.Domain.Alquileres.Events;

public sealed record AlquilerConfirmadoDomainEvent(AlquilerId AlquilerId) : IDomainEvents;