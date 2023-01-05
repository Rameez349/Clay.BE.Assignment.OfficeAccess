using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions.Types;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Constants;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<string> AuthenticateUser(long userId)
        {
            if (!await _userRepository.DoesExist(userId))
                throw new NotFoundException($"{ApiResponseMessages.Notfound}: User ID: {userId}");

            return _tokenService.GenerateJwtToken(userId, string.Empty);
        }
    }
}
