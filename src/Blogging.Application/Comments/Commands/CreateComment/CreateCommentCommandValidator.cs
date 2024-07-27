using FluentValidation;

namespace Blogging.Application.Comments.Commands.CreateComment;
public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(dto => dto.Text).NotEmpty();
    }
}
