using Blogging.Application.Authentication.DTOs;

namespace Blogging.Application.Interfaces;
public interface IAuthenticationService
{
    Task<AuthResponseDto> Login(LoginDto loginDto);
    Task<AuthResponseDto> Register(RegisterDto registerDto);
}