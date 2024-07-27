using Blogging.Application.Posts.DTOs;
using MediatR;

namespace Blogging.Application.Posts.Queries.GetPostById;
public class GetPostByIdQuery : IRequest<PostDto?>
{
    /*
     * store the information about the ID for a particular restaurant that we want to return to the client.
     */
    public int Id { get;}
    public GetPostByIdQuery(int id)
    {
        Id = id;
    }
}
