using Application.Dtos.Responses;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;


public class DoorsRepository : IDoorsRepository
{
    private readonly OfficeAccessDbContext _officeAccessDbContext;

    public DoorsRepository(OfficeAccessDbContext officeAccessDbContext)
    {
        _officeAccessDbContext = officeAccessDbContext;
    }

    public Task InsertAsync(Door entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Door entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Door entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AddDoorAccessHistoryAsync(long userId, long doorId, bool accessGranted)
    {
        if (!await _officeAccessDbContext.Users.AnyAsync(x => x.Id == userId) ||
               !await _officeAccessDbContext.Doors.AnyAsync(x => x.Id == doorId))
            return false;

        _officeAccessDbContext.Add(new AccessHistory
        {
            UserId = userId,
            DoorId = doorId,
            AccessGranted = accessGranted,
            Timestamp = DateTime.UtcNow
        });

        return await _officeAccessDbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> AuthorizeDoorAccessAsync(long userId, long doorId)
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
             .Where(x => x.UserId == userId && x.DoorId == doorId).AnyAsync();
    }

    public async Task<IEnumerable<AccessHistory>> GetDoorAccessHistoryAsync(long doorId)
    {
        return await _officeAccessDbContext.AccessHistory.Where(x => x.DoorId == doorId).Include(x => x.Door).Include(x => x.User).ToListAsync();
    }
}
