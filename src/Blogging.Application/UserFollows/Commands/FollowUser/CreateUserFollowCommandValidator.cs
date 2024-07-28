using FluentValidation;

namespace Blogging.Application.UserFollows.Commands.FollowUser;
public class CreateUserFollowCommandValidator : AbstractValidator<CreateUserFollowCommand>
{
    public CreateUserFollowCommandValidator()
    {
        RuleFor(dto => dto.FolloweeId).NotEmpty();
    }
}
