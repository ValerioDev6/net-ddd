using CleanArquitectura.Application.Abstractions;
using CleanArquitectura.Domain.Alquileres;

namespace CleanArquitectura.Application.Alquileres.GetAlquiler;

public sealed record GetAlquilerQuery(Guid AlquilerId):IQuery<AlquilerResponse>;