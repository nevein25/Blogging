using Blogging.Application.UserFollows.DTOs;
using MediatR;

namespace Blogging.Application.UserFollows.Queries.GetAllFollowers;
public class GetAllCurrentUserFollowersQuery : IRequest<IEnumerable<CurrentUserFollowersDto>>
{
}