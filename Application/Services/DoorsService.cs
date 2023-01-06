using Application.Dtos.Responses;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Mappings;
using Domain.Entities;

namespace Application.Services;

public class DoorsService : IDoorsService
{
    private readonly IDoorsRepository _doorsRepository;

    public DoorsService(IDoorsRepository doorsRepository)
    {
        _doorsRepository = doorsRepository;
    }

    public async Task<AccessResponse> AuthorizeDoorAccessAsync(long userId, long doorId)
    {
        return new AccessResponse
        {
            UserId = userId,
            DoorId = doorId,
            AccessGranted = await _doorsRepository.AuthorizeDoorAccessAsync(userId, doorId)
        };
    }

    public async Task<AccessResponse> AuthorizeViewDoorAccessHistoryAsync(long userId, long doorId)
    {
        return new AccessResponse
        {
            UserId = userId,
            DoorId = doorId,
            AccessGranted = await _doorsRepository.AuthorizeViewDoorAccessHistoryAsync(userId, doorId)
        };
    }

    public Task<bool> AddDoorAccessHistoryAsync(long userId, long doorId, bool accessGranted)
    {
        return _doorsRepository.AddDoorAccessHistoryAsync(userId, doorId, accessGranted);
    }

    public async Task<IEnumerable<AccessHistoryResponse>> GetDoorAccessHistoryAsync(long doorId)
    {
        var accessHistory = await _doorsRepository.GetDoorAccessHistoryAsync(doorId);

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
