using System;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly OfficeAccessDbContext _officeAccessDbContext;

        public UserRepository(OfficeAccessDbContext officeAccessDbContext)
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
        public async Task<bool> DoesExist(long id)
        {
            return await _officeAccessDbContext.Users.AnyAsync(x => x.Id == id);
        }
    }
}
