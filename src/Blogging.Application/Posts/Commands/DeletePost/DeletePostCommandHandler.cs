using AutoMapper;
using Blogging.Application.Posts.Commands.CreatePost;
using Blogging.Application.Users;
using Blogging.Domain.Entities;
using Blogging.Domain.Exceptions;
using Blogging.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blogging.Application.Posts.Commands.DeletePost;
public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
{
    private readonly IPostsRepository _postsRepository;
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;
    private readonly ILogger<DeletePostCommandHandler> _logger;

    public DeletePostCommandHandler(IPostsRepository postsRepository,
                                    IMapper mapper,
                                    IUserContext userContext,
                                    ILogger<DeletePostCommandHandler> logger)
    {
        _postsRepository = postsRepository;
        _mapper = mapper;
        _userContext = userContext;
        _logger = logger;
    }

    public async Task Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting post with id: {PostId}", request.Id);
        var post = await _postsRepository.GetByIdAsync(request.Id);
        if (post is null)
            throw new NotFoundException(nameof(Post), request.Id.ToString());

        int? loggedInUserId = _userContext.UserId;
        if (loggedInUserId != post.UserId) throw new ForbidException("You can not delete post you do not own");

        await _postsRepository.Delete(post);
    }
}
