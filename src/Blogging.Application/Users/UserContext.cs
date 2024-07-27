using Blogging.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Blogging.Application.Users;

public interface IUserContext
{
    string? Email { get; }
    ClaimsPrincipal? User { get; }
    int? UserId { get; }
    string? UserName { get; }
}

internal class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    public int? UserId
    {
        get
        {
            return int.TryParse(User?.FindFirstValue(ClaimTypes.NameIdentifier), out var userId) ? userId : null;
        }
    }

    public string? UserName => User?.FindFirstValue(ClaimTypes.Name);

    public string? Email => User?.FindFirstValue(ClaimTypes.Email);


}
