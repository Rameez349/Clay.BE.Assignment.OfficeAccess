using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.Responses;

namespace Application.Interfaces
{
    public interface IUserAccessService
    {
        Task<AccessResponse> AuthorizeUserAccessAsync(int userId, int doorId);
        Task<AccessResponse> AuthorizeViewAccessHistoryAsync(int userId, int doorId);
    }
}
