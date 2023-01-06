using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.Responses;

namespace Application.Interfaces
{
    public interface IDoorsService
    {
        Task<AccessResponse> AuthorizeDoorAccessAsync(long userId, long doorId);
        Task<IEnumerable<AccessHistoryResponse>> GetDoorAccessHistoryAsync(long doorId);
        Task<bool> AddDoorAccessHistoryAsync(long userId, long doorId, bool accessGranted);
    }
}
