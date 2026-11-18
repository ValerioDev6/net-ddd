namespace CleanArquitectura.Application.Exceptions;

public sealed class ConcurrencyExceptions : Exception
{
    public ConcurrencyExceptions(string message, Exception innerException) : base(message, innerException)
    {
        
    }
    
}