using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace Infrastructure.Persistence.Tests.DbSetMocks
{
    public static class DoorMock
    {
        public static Mock<DbSet<Door>> GetDoorMockData()
        {
            var users = new List<Door>();
            users.Add(new Door { Id = 1, Name = "Entrance" });
            users.Add(new Door { Id = 2, Name = "Storage" });

            return users.AsQueryable().BuildMockDbSet();
        }
    }
}
