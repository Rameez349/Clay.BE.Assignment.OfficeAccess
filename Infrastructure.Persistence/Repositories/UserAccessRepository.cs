using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class UserAccessRepository : IUserAccessRepository
    {
        private readonly OfficeAccessDbContext _officeAccessDbContext;

        public UserAccessRepository(OfficeAccessDbContext officeAccessDbContext)
        {
            _officeAccessDbContext = officeAccessDbContext;
        }

        public async Task<bool> AuthorizeUserAccessAsync(int UserId, int DoorId)
        {
            return await _officeAccessDbContext.UserAccessLevels.Join(
                _officeAccessDbContext.DoorAccessLevels,
                userAccess => userAccess.AccessLevelId,
                doorAccess => doorAccess.AccessLevelId,
                (userAccess, doorAccess) => new { userAccess, doorAccess }).Select(x => new
                {
                    x.userAccess.UserId,
                    x.doorAccess.DoorId,
                })
                .Where(x => x.UserId == UserId && x.DoorId == DoorId).AnyAsync();
        }

        public async Task<bool> AuthorizeViewAccessHistoryAsync(int UserId, int DoorId)
        {
            return await _officeAccessDbContext.UserAccessLevels.Join(
                _officeAccessDbContext.DoorAccessLevels,
                userAccess => userAccess.AccessLevelId,
                doorAccess => doorAccess.AccessLevelId,
                (userAccess, doorAccess) => new { userAccess, doorAccess }).Select(x => new
                {
                    x.userAccess.UserId,
                    x.doorAccess.DoorId,
                    x.userAccess.User.AllowHistoryView
                })
                .Where(x => x.UserId == UserId && x.DoorId == DoorId && x.AllowHistoryView).AnyAsync();
        }
    }
}
