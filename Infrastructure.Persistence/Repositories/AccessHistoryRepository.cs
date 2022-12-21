using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class AccessHistoryRepository : IAccessHistoryRepository
    {
        private readonly OfficeAccessDbContext _officeAccessDbContext;

        public AccessHistoryRepository(OfficeAccessDbContext officeAccessDbContext)
        {
            _officeAccessDbContext = officeAccessDbContext;
        }

        public async Task<bool> AddAccessHistoryAsync(int userId, int doorId, bool accessGranted)
        {
            _officeAccessDbContext.Add(new AccessHistory
            {
                UserId = userId,
                DoorId = doorId,
                AccessGranted = accessGranted,
                Timestamp = DateTime.UtcNow
            });

            return await _officeAccessDbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<AccessHistory>> GetAccessHistoryAsync(int doorId)
        {
            return await _officeAccessDbContext.AccessHistory.Where(x => x.DoorId == doorId).Include(x => x.Door).Include(x => x.User).ToListAsync();
        }
    }
}
