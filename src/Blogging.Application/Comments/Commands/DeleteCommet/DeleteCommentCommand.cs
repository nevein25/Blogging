using MediatR;


namespace Blogging.Application.Comments.Commands.DeleteCommet;
public class DeleteCommentCommand : IRequest
{
    public int Id { get; }
    public DeleteCommentCommand(int id)
    {
        Id = id;
    }
}
