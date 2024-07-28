using Blogging.Domain.Entities;
using MediatR;

namespace Blogging.Application.UserFollows.Commands.FollowUser;
public class CreateUserFollowCommand : IRequest<int>
{
    public int FolloweeId { get; set; }
}
