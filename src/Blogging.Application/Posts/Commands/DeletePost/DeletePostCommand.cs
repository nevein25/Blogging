using MediatR;

namespace Blogging.Application.Posts.Commands.DeletePost;
public class DeletePostCommand : IRequest
{
    public int Id { get; }
    public DeletePostCommand(int id)
    {
        Id = id;
    }
}
