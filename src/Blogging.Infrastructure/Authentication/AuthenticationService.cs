
using Blogging.Application.Authentication.DTOs;
using Blogging.Application.Exceptions;
using Blogging.Application.Interfaces;
using Blogging.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blogging.Infrastructure.Authentication;
internal class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenGenerator _tokenGenerator;

    public AuthenticationService(UserManager<User> userManager, ITokenGenerator tokenGenerator)
    {
        _userManager = userManager;
        _tokenGenerator = tokenGenerator;
    }
    public async Task<AuthResponseDto> Login(LoginDto loginDto)
    {
        User? user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == loginDto.Username);


        if (user == null) throw new InvalidLoginException();

        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!result) throw new InvalidLoginException();

        var token = _tokenGenerator.GenerateToken(user.Id, user.UserName!, user.Email!);
        return new AuthResponseDto(token);
    }

    public async Task<AuthResponseDto> Register(RegisterDto registerDto)
    {
        if (await UserEmailExists(registerDto.Email)) throw new EmailAlreadyRegisteredException();
        if (await UserUsernameExists(registerDto.Username)) throw new UsernameTakenException();

        User user = new User { Email = registerDto.Email, UserName = registerDto.Username };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded) throw new Exception(result.Errors.FirstOrDefault()?.Description);


        var token = _tokenGenerator.GenerateToken(user.Id, user.UserName!, user.Email!);
        return new AuthResponseDto(token);
    }
    private async Task<bool> UserEmailExists(string email)
    {
        return await _userManager.Users.AnyAsync(x => x.Email == email.ToLower());
    }

    private async Task<bool> UserUsernameExists(string username)
    {
        return await _userManager.Users.AnyAsync(x => x.NormalizedUserName == username.ToUpper());
    }
}
