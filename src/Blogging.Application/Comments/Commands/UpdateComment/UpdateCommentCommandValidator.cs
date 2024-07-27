using Blogging.Application.Comments.Commands.CreateComment;
using FluentValidation;

namespace Blogging.Application.Comments.Commands.UpdateComment;
public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentCommandValidator()
    {
        RuleFor(dto => dto.Text).NotEmpty();
    }
}