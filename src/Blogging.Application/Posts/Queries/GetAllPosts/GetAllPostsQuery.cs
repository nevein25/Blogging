using Blogging.Application.Posts.DTOs;
using Blogging.Domain.Entities;
using MediatR;

namespace Blogging.Application.Posts.Queries.GetAllPosts;
public class GetAllPostsQuery : IRequest<IEnumerable<PostDto>>
{

}
