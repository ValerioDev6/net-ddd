using CleanArquitectura.Domain.Abstraccions;
using MediatR;

namespace CleanArquitectura.Application.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }