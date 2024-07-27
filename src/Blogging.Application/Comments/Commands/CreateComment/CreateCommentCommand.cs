using MediatR;

namespace Blogging.Application.Comments.Commands.CreateComment;
public class CreateCommentCommand : IRequest<int>
{
    public string Text { get; set; } = default!;
    public int PostId { get; set; }

}
