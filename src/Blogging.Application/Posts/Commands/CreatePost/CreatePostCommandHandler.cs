using AutoMapper;
using Blogging.Application.Users;
using Blogging.Domain.Entities;
using Blogging.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blogging.Application.Posts.Commands.CreatePost;
internal class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, int>
{
    private readonly IPostsRepository _postsRepository;
    private readonly IUserContext _userContext;
    private readonly IMapper _mapper;
    private readonly ILogger<CreatePostCommandHandler> _logger;

    public CreatePostCommandHandler(IPostsRepository postsRepository,
                                    IUserContext userContext,
                                    IMapper mapper,
                                    ILogger<CreatePostCommandHandler> logger)
    {
        _postsRepository = postsRepository;
        _userContext = userContext;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        var post = _mapper.Map<Post>(request);

        if (userId == null) throw new InvalidOperationException("User Id is not available.");

        _logger.LogInformation("[{UserId}] is creating a new post {@Post}",
           userId,
           request);

        post.UserId = userId.Value;

        int postId = await _postsRepository.Create(post);
        return postId;
    }
}
