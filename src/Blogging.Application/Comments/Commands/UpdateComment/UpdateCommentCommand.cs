using MediatR;

namespace Blogging.Application.Comments.Commands.UpdateComment;
public class UpdateCommentCommand : IRequest
{
    public int Id { get; set; }
    public string Text { get; set; } = default!;

}
