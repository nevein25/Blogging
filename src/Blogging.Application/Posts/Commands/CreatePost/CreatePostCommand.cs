using MediatR;

namespace Blogging.Application.Posts.Commands.CreatePost;
public class CreatePostCommand : IRequest<int>
{
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
}
