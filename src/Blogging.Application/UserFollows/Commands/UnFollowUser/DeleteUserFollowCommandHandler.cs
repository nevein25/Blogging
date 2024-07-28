using Blogging.Application.Exceptions;
using Blogging.Application.Users;
using Blogging.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blogging.Application.UserFollows.Commands.UnFollowUser;
internal class DeleteUserFollowCommandHandler : IRequestHandler<DeleteUserFollowCommand>
{
    private readonly IUserFollowsRepository _userFollowsRepository;
    private readonly IUserContext _userContext;
    private readonly ILogger<DeleteUserFollowCommandHandler> _logger;

    public DeleteUserFollowCommandHandler(IUserFollowsRepository userFollowsRepository,
                                          IUserContext userContext,
                                          ILogger<DeleteUserFollowCommandHandler> logger)
    {
        _userFollowsRepository = userFollowsRepository;
        _userContext = userContext;
        _logger = logger;
    }

    public async Task Handle(DeleteUserFollowCommand request, CancellationToken cancellationToken)
    {
        var followerId = _userContext.UserId;
        if (followerId is null) throw new InvalidOperationException("User Id is not available.");

        _logger.LogInformation("[{FollowerId}] is now unfollowing [{FolloweeId}]", followerId, request.FolloweeId);


        var userFollow = await _userFollowsRepository.GetById(followerId.Value, request.FolloweeId);
        if (userFollow is null) throw new NotFollowingException();

        await _userFollowsRepository.Delete(userFollow);
    }
}
