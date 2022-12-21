using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace Infrastructure.Persistence.Tests.DbSetMocks
{
    public static class DoorAccessLevelMock
    {
        public static Mock<DbSet<DoorAccessLevel>> GetDoorAccessLevelMockData()
        {
            var doorAccessLevelMocks = new List<DoorAccessLevel>();

            doorAccessLevelMocks.Add(new DoorAccessLevel { DoorId = 1, AccessLevelId = 1 });
            doorAccessLevelMocks.Add(new DoorAccessLevel { DoorId = 2, AccessLevelId = 2 });

            return doorAccessLevelMocks.AsQueryable().BuildMockDbSet();
        }
    }
}
