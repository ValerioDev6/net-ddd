namespace CleanArquitectura.Application.Exceptions;

public sealed record ValidationError(string Property, string ErrorMessage);