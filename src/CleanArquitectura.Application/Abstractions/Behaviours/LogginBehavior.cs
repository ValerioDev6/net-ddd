using CleanArquitectura.Domain.Abstraccions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArquitectura.Application.Abstractions.Behaviours;

public class LogginBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : IBaseRequest
where TResponse : Result
{
    private readonly ILogger<LogginBehavior<TRequest, TResponse>> _logger;

    public LogginBehavior(ILogger<LogginBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var name = request.GetType().Name;

        try
        {
            _logger.LogInformation($"Executando {name}");
            var result = await next();
            _logger.LogInformation($"Resultado {result}");

            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error {name}");
            throw;
        }


    }
}
