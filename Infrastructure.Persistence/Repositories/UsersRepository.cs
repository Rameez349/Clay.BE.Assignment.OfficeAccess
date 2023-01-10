using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly OfficeAccessDbContext _officeAccessDbContext;

    public UsersRepository(OfficeAccessDbContext officeAccessDbContext)
    {
        _officeAccessDbContext = officeAccessDbContext;
    }

    public Task InsertAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public async Task<User> DoesExist(long id)
    {
        return await _officeAccessDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
    }
}
