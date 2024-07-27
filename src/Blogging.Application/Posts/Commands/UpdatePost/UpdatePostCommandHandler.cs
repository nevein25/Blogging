

using AutoMapper;
using Blogging.Application.Posts.Commands.CreatePost;
using Blogging.Application.Users;
using Blogging.Domain.Exceptions;
using Blogging.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blogging.Application.Posts.Commands.UpdatePost;
public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand>
{
    private readonly IPostsRepository _postsRepository;
    private readonly IUserContext _userContext;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdatePostCommandHandler> _logger;

    public UpdatePostCommandHandler(IPostsRepository postsRepository,
                                    IUserContext userContext,
                                    IMapper mapper,
                                    ILogger<UpdatePostCommandHandler> logger)
    {
        _postsRepository = postsRepository;
        _userContext = userContext;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating post with id: {OostId} with {@UpdatedPost}", request.Id, request);
        var post = await _postsRepository.GetByIdAsync(request.Id);
        if (post is null)
            throw new NotFoundException(nameof(post), request.Id.ToString());

        int? loggedInUserId = _userContext.UserId;
        if (loggedInUserId != post.UserId) throw new ForbidException("You can not update post you do not own");
        _mapper.Map(request, post);


        await _postsRepository.SaveChanges();
    }
}
