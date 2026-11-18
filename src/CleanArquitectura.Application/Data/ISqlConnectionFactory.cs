using System.Data;

namespace CleanArquitectura.Application.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}