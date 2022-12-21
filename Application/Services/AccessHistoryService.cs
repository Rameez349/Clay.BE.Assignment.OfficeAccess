using Application.Dtos.Responses;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Mappings;
using Domain.Entities;

namespace Application.Services
{
    public class AccessHistoryService : IAccessHistoryService
    {
        private readonly IAccessHistoryRepository _accessHistoryRepository;

        public AccessHistoryService(IAccessHistoryRepository accessHistoryRepository)
        {
            _accessHistoryRepository = accessHistoryRepository;
        }

        public Task<bool> AddAccessHistoryAsync(int userId, int doorId, bool accessGranted)
        {
            return _accessHistoryRepository.AddAccessHistoryAsync(userId, doorId, accessGranted);
        }

        public async Task<IEnumerable<AccessHistoryResponse>> GetAccessHistoryAsync(int doorId)
        {
            var accessHistory = await _accessHistoryRepository.GetAccessHistoryAsync(doorId);

            return MapToAccessHistoryResponse(accessHistory);
        }

        private IEnumerable<AccessHistoryResponse> MapToAccessHistoryResponse(IEnumerable<AccessHistory> accessHistory)
        {
            foreach (var entry in accessHistory)
            {
                yield return entry.MapToAccessHistoryResponseDto();
            }
        }
    }
}
