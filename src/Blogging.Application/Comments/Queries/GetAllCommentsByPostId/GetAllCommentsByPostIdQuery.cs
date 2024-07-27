using Blogging.Application.Comments.DTOs;
using Blogging.Application.Posts.DTOs;
using MediatR;


namespace Blogging.Application.Comments.Queries.GetAllCommentsByPostId;
public class GetAllCommentsByPostIdQuery : IRequest<IEnumerable<CommentDto>>
{
    public int Id { get; }
    public GetAllCommentsByPostIdQuery(int id)
    {
        Id = id;
    }
}
