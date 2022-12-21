using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.Responses;

namespace Application.Interfaces
{
    public interface IAccessHistoryService
    {
        Task<IEnumerable<AccessHistoryResponse>> GetAccessHistoryAsync(int doorId);
        Task<bool> AddAccessHistoryAsync(int userId, int doorId, bool accessGranted);
    }
}
