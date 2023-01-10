using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface IDoorsRepository
{
    Task<bool> AuthorizeDoorAccessAsync(long userId, long doorId);
    Task<IEnumerable<AccessHistory>> GetDoorAccessHistoryAsync(long doorId);
    Task<bool> AddDoorAccessHistoryAsync(long userId, long doorId, bool accessGranted);
}
