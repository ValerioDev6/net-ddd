using CleanArquitectura.Domain.Abstraccions;
using MediatR;

namespace CleanArquitectura.Application.Abstractions;

public interface ICommandHandler<TCommand>: IRequestHandler<TCommand, Result> where TCommand : ICommand
{
    
}


public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>> where TCommand : ICommand<TResponse>{}