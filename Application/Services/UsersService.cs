using Application.Interfaces;
using Domain.Constants;
using Domain.Exceptions;
using Domain.RepositoryInterfaces;

namespace Application.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _userRepository;
    private readonly ITokenService _tokenService;

    public UsersService(IUsersRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<string> AuthenticateUser(long userId)
    {
        var user = await _userRepository.DoesExist(userId);
        if (user is null)
            throw new NotFoundException($"{ApiResponseMessages.Notfound}: User ID: {userId}");

        return _tokenService.GenerateJwtToken(userId, user.Name, user.AllowHistoryView);
    }
}
