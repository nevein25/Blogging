using Blogging.Application.Authentication.DTOs;
using FluentValidation;

namespace Blogging.Application.Authentication.Validators;
public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(dto => dto.Email).EmailAddress().WithMessage("Please Provide valid email address."); ;
        RuleFor(dto => dto.Password).Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{6,12}$")
                                    .WithMessage("Password must be 6-12 characters long, with at least one digit, one uppercase letter, and one lowercase letter.");
    }
}

