using AutoMapper;
using Blogging.Application.Exceptions;
using Blogging.Application.Posts.Commands.CreatePost;
using Blogging.Application.Users;
using Blogging.Domain.Entities;
using Blogging.Domain.Exceptions;
using Blogging.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Blogging.Application.UserFollows.Commands.FollowUser;
internal class CreateUserFollowCommandHandler : IRequestHandler<CreateUserFollowCommand, int>
{
    private readonly IUserFollowsRepository _userFollowsRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly IUserContext _userContext;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateUserFollowCommandHandler> _logger;

    public CreateUserFollowCommandHandler(IUserFollowsRepository userFollowsRepository,
                                          IUsersRepository usersRepository, 
                                          IUserContext userContext,
                                          IMapper mapper,
                                          ILogger<CreateUserFollowCommandHandler> logger)
    {
        _userFollowsRepository = userFollowsRepository;
        _usersRepository = usersRepository;
        _userContext = userContext;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<int> Handle(CreateUserFollowCommand request, CancellationToken cancellationToken)
    {
        var followerId = _userContext.UserId;
        if (followerId == null) throw new InvalidOperationException("User Id is not available.");

        if (await _userFollowsRepository.IsFollowing(followerId.Value, request.FolloweeId))
            throw new AlreadyFollowingException();

        bool isFolloweeExist = await _usersRepository.IsUserExistById(request.FolloweeId);
        if (!isFolloweeExist) throw new InvalidOperationException("User Id is not available.");

        if (followerId == request.FolloweeId) throw new SelfFollowException();

        var userFollow = _mapper.Map<UserFollow>(request);

        userFollow.FollowerId = followerId.Value;

        _logger.LogInformation("[{FollowerId}] is now following [{FolloweeId}]", followerId, request.FolloweeId);




        int followeeId = await _userFollowsRepository.Create(userFollow);
        return followeeId;
    }
}
