namespace Application.Interfaces;
public interface ITokenService
{
    string GenerateJwtToken(long userId, string username);
}
