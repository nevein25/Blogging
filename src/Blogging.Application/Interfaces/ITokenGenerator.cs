namespace Blogging.Application.Interfaces;
public interface ITokenGenerator
{
    string GenerateToken(int id, string username, string email);
}
