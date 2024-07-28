using AutoMapper;
using Blogging.Application.Comments.DTOs;
using Blogging.Application.UserFollows.Commands.FollowUser;
using Blogging.Application.UserFollows.DTOs;
using Blogging.Application.Users;
using Blogging.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blogging.Application.UserFollows.Queries.GetAllFollowers;
internal class GetAllCurrentUserFollowersQueryHandler : IRequestHandler<GetAllCurrentUserFollowersQuery, IEnumerable<CurrentUserFollowersDto>>
{
    private readonly IUserFollowsRepository _userFollowsRepository;
    private readonly IUserContext _userContext;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateUserFollowCommandHandler> _logger;

    public GetAllCurrentUserFollowersQueryHandler(IUserFollowsRepository userFollowsRepository,
                                          IUserContext userContext,
                                          IMapper mapper,
                                          ILogger<CreateUserFollowCommandHandler> logger)
    {
        _userFollowsRepository = userFollowsRepository;
        _userContext = userContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<CurrentUserFollowersDto>> Handle(GetAllCurrentUserFollowersQuery request, CancellationToken cancellationToken)
    {
        int? currentUserId = _userContext.UserId;
        if (currentUserId is null) throw new InvalidOperationException("User Id is not available.");

        _logger.LogInformation("Getting all followers");
        var followeing = await _userFollowsRepository.GetUsersThatCurrentUserIsFollowing(currentUserId.Value);
      
        var currentUserFollowersDto = _mapper.Map<IEnumerable<CurrentUserFollowersDto>>(followeing);
        return currentUserFollowersDto;
    }
}
