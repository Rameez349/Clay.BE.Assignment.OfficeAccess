using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface IUsersRepository : IRepository<User>
{
    Task<User> DoesExist(long id);
}
