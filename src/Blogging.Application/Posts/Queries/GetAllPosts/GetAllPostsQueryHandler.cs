using AutoMapper;
using Blogging.Application.Common;
using Blogging.Application.Posts.DTOs;
using Blogging.Application.Posts.Queries.GetPostById;
using Blogging.Application.Users;
using Blogging.Domain.Entities;
using Blogging.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blogging.Application.Posts.Queries.GetAllPosts;
public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, PagedResult<PostDto>>
{
    private readonly IPostsRepository _postsRepository;
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;
    private readonly IUserFollowsRepository _followsRepository;
    private readonly ILogger<GetAllPostsQueryHandler> _logger;

    public GetAllPostsQueryHandler(IPostsRepository postsRepository,
                                   IMapper mapper,
                                   IUserContext userContext,
                                   IUserFollowsRepository followsRepository,
                                   ILogger<GetAllPostsQueryHandler> logger)
    {
        _postsRepository = postsRepository;
        _mapper = mapper;
        _userContext = userContext;
        _followsRepository = followsRepository;
        _logger = logger;
    }

    //public async Task<IEnumerable<PostDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    //{
    //    _logger.LogInformation("Getting all posts");

    //    var posts = await _postsRepository.GetAll();
    //    var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);

    //    return postsDto;
    //}
    public async Task<PagedResult<PostDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        var loggedInUserId = _userContext.UserId;
        if (loggedInUserId == null) throw new InvalidOperationException("User Id is not available.");

        var followingIds = await _followsRepository.GetUserIdsThatCurrentUserIsFollowing(loggedInUserId.Value);

        var (posts, totalCount) = await _postsRepository.GetFolloweePostsMatching(followingIds, request.SearchPhraseTitle,
                                                                                    request.SearchPhraseAuthor,
                                                                                    request.PageSize, request.PageNumber,
                                                                                    request.SortBy, request.SortDirection);

        _logger.LogInformation($"Getting posts");

        var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);

        var result = new PagedResult<PostDto>(postsDto, totalCount, request.PageSize, request.PageNumber);
        return result;
    }
}
