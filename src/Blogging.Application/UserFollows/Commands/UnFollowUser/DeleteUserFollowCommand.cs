using MediatR;

namespace Blogging.Application.UserFollows.Commands.UnFollowUser;
public class DeleteUserFollowCommand : IRequest
{
    public int FolloweeId { get; }
    public DeleteUserFollowCommand(int followeeId)
    {
        FolloweeId = followeeId;
    }
}
