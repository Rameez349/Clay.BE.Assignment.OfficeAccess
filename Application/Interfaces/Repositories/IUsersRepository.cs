using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IUsersRepository : IRepository<User>
    {
        Task<User> DoesExist(long id);
    }
}
