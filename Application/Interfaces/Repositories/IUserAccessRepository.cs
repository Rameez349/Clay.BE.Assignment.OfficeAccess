namespace Application.Interfaces.Repositories
{
    public interface IUserAccessRepository
    {
        Task<bool> AuthorizeUserAccessAsync(int UserId, int DoorId);
        Task<bool> AuthorizeViewAccessHistoryAsync(int UserId, int DoorId);
    }
}
