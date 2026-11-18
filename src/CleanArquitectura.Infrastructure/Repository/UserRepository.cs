using CleanArquitectura.Application.Paginations;
using CleanArquitectura.Domain.Users;
using CleanArquitectura.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArquitectura.Infrastructure.Repository;

internal sealed class UserRepository : Repository<User, UserId>, IUserRepository, IPaginationRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }


    public async Task<User?> GetByEmailAsync(Domain.Users.Email email, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<User>()
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }
}
