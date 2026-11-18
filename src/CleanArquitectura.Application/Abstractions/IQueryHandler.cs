using CleanArquitectura.Domain.Abstraccions;
using MediatR;

namespace CleanArquitectura.Application.Abstractions;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse>
{
    
}