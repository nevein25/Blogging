using AutoMapper;
using Blogging.Application.Posts.Commands.CreatePost;
using Blogging.Application.Users;
using Blogging.Domain.Entities;
using Blogging.Domain.Exceptions;
using Blogging.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blogging.Application.Comments.Commands.CreateComment;
internal class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, int>
{
    private readonly ICommentsRepository _commentsRepository;
    private readonly IPostsRepository _postsRepository;
    private readonly IUserContext _userContext;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCommentCommandHandler> _logger;

    public CreateCommentCommandHandler(ICommentsRepository commentsRepository,
                                       IPostsRepository postsRepository,
                                    IUserContext userContext,
                                    IMapper mapper,
                                    ILogger<CreateCommentCommandHandler> logger)
    {
        _commentsRepository = commentsRepository;
        _postsRepository = postsRepository;
        _userContext = userContext;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<int> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;

        if (userId == null) throw new InvalidOperationException("User Id is not available.");

        var post = await _postsRepository.GetByIdAsync(request.PostId);
        if (post == null) throw new NotFoundException(nameof(Post), request.PostId.ToString());
    
        _logger.LogInformation("[{UserId}] is creating a new comment {@Comment}",
           userId,
           request);

        var comment = _mapper.Map<Comment>(request);

        comment.UserId = userId.Value;

        int commentId = await _commentsRepository.Create(comment);
        return commentId;
    }
}
