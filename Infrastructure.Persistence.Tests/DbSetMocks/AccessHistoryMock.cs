using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace Infrastructure.Persistence.Tests.DbSetMocks
{
    public static class AccessHistoryMock
    {
        public static Mock<DbSet<AccessHistory>> GetAccessHistoryMockData(DateTime timeStamp)
        {
            var accesshistoryMocks = new List<AccessHistory>();

            accesshistoryMocks.Add(new AccessHistory { UserId = 1, DoorId = 1, AccessGranted = true, Timestamp = timeStamp, Door = new Door { Id = 1, Name = "Entrance" }, User = new User { Id = 1, Name = "Rameez" } });
            accesshistoryMocks.Add(new AccessHistory { UserId = 1, DoorId = 2, AccessGranted = true, Timestamp = timeStamp, Door = new Door { Id = 1, Name = "Storage" }, User = new User { Id = 1, Name = "Rameez" } });
            accesshistoryMocks.Add(new AccessHistory { UserId = 2, DoorId = 2, AccessGranted = true, Timestamp = timeStamp, Door = new Door { Id = 1, Name = "Storage" }, User = new User { Id = 2, Name = "Darjan" } });

            return accesshistoryMocks.AsQueryable().BuildMockDbSet();
        }
    }
}
