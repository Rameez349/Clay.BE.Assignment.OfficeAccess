using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace Infrastructure.Persistence.Tests.DbSetMocks
{
    public static class UserMock
    {
        public static Mock<DbSet<User>> GetUserMockData()
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Name = "Rameez", AllowHistoryView = true });
            users.Add(new User { Id = 3, Name = "Lucas", AllowHistoryView = false });

            return users.AsQueryable().BuildMockDbSet();
        }
    }
}
