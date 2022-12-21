using Application.Dtos.Responses;
using Application.Interfaces;
using Application.Interfaces.Repositories;

namespace Application.Services
{
    public class UserAccessService : IUserAccessService
    {
        private readonly IUserAccessRepository _userAccessRepository;

        public UserAccessService(IUserAccessRepository userAccessRepository)
        {
            _userAccessRepository = userAccessRepository;
        }

        public async Task<AccessResponse> AuthorizeUserAccessAsync(int userId, int doorId)
        {
            return new AccessResponse
            {
                UserId = userId,
                DoorId = doorId,
                AccessGranted = await _userAccessRepository.AuthorizeUserAccessAsync(userId, doorId)
            };
        }

        public async Task<AccessResponse> AuthorizeViewAccessHistoryAsync(int userId, int doorId)
        {
            return new AccessResponse
            {
                UserId = userId,
                DoorId = doorId,
                AccessGranted = await _userAccessRepository.AuthorizeViewAccessHistoryAsync(userId, doorId)
            };
        }
    }
}
