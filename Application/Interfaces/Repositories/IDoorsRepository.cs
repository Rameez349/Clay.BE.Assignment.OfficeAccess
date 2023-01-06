using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IDoorsRepository : IRepository<Door>
    {
        Task<bool> AuthorizeDoorAccessAsync(long userId, long doorId);
        Task<bool> AuthorizeViewDoorAccessHistoryAsync(long userId, long doorId);
        Task<IEnumerable<AccessHistory>> GetDoorAccessHistoryAsync(long doorId);
        Task<bool> AddDoorAccessHistoryAsync(long userId, long doorId, bool accessGranted);
    }
}
