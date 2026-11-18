using CleanArquitectura.Domain.Abstraccions;

namespace CleanArquitectura.Domain.Alquileres.Events;

public record AlquilerCompletadoDomainEvent(AlquilerId AlquilerId): IDomainEvents;