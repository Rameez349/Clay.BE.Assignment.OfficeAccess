using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IAccessHistoryRepository
    {
        Task<bool> AddAccessHistoryAsync(int userId, int doorId, bool accessGranted);
        Task<IEnumerable<AccessHistory>> GetAccessHistoryAsync(int doorId);
    }
}
