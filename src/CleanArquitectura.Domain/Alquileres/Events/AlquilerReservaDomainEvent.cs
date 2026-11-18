using CleanArquitectura.Domain.Abstraccions;

namespace CleanArquitectura.Domain.Alquileres.Events;

public sealed record AlquilerReservaDomainEvent(AlquilerId AlquilerId): IDomainEvents;