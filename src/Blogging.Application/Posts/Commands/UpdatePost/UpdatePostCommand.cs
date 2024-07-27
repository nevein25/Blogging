using MediatR;


namespace Blogging.Application.Posts.Commands.UpdatePost;
public class UpdatePostCommand : IRequest
{
    public int Id { get; set; }

    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
}
