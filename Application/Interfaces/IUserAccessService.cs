using Application.Dtos.Responses;

namespace Application.Interfaces
{
    public interface IUserAccessService
    {
        Task<AccessResponse> AuthorizeUserAccessAsync(int userId, int doorId);
        Task<AccessResponse> AuthorizeViewAccessHistoryAsync(int userId, int doorId);
    }
}
