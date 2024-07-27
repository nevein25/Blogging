using FluentValidation;

namespace Blogging.Application.Posts.Commands.UpdatePost;
public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
{
    public UpdatePostCommandValidator()
    {
        RuleFor(dto => dto.Content).NotEmpty();

        RuleFor(dto => dto.Title).NotEmpty();
    }
}
