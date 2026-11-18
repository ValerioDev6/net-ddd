namespace CleanArquitectura.Domain.Abstraccions;

public interface IEntity
{

    IReadOnlyList<IDomainEvents> GetDomainEvents();

    void ClearDomainEvents();

}