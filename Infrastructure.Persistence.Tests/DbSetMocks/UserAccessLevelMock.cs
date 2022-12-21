using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace Infrastructure.Persistence.Tests.DbSetMocks
{
    public static class UserAccessLevelMock
    {
        public static Mock<DbSet<UserAccessLevel>> GetUserAccessLevelMockData()
        {
            var userAccessLevels = new List<UserAccessLevel>();
            userAccessLevels.Add(new UserAccessLevel { UserId = 1, AccessLevelId = 1, User = new User { AllowHistoryView = true } });
            userAccessLevels.Add(new UserAccessLevel { UserId = 1, AccessLevelId = 2, User = new User { AllowHistoryView = true } });
            userAccessLevels.Add(new UserAccessLevel { UserId = 2, AccessLevelId = 1, User = new User { AllowHistoryView = false } });

            return userAccessLevels.AsQueryable().BuildMockDbSet();
        }
    }
}
