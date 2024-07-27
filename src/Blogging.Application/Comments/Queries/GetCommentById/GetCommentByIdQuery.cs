using Blogging.Application.Comments.DTOs;
using MediatR;

namespace Blogging.Application.Comments.Queries.GetCommentById;
public class GetCommentByIdQuery : IRequest<CommentDto?>
{
    public int Id { get; }
    public GetCommentByIdQuery(int id)
    {
        Id = id;
    }
}
