using AutoMapper;
using Blogging.Application.Posts.Commands.UpdatePost;
using Blogging.Application.Users;
using Blogging.Domain.Exceptions;
using Blogging.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blogging.Application.Comments.Commands.UpdateComment;
internal class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand>
{
    private readonly ICommentsRepository _commentsRepository;
    private readonly IUserContext _userContext;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateCommentCommandHandler> _logger;

    public UpdateCommentCommandHandler(ICommentsRepository commentsRepository,
                                    IUserContext userContext,
                                    IMapper mapper,
                                    ILogger<UpdateCommentCommandHandler> logger)
    {
        _commentsRepository = commentsRepository;
        _userContext = userContext;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating Comment with id: {OostId} with {@UpdatedComment}", request.Id, request);
        var comment = await _commentsRepository.GetByIdAsync(request.Id);
        if (comment is null)
            throw new NotFoundException(nameof(comment), request.Id.ToString());

        int? loggedInUserId = _userContext.UserId;
        if (loggedInUserId != comment.UserId) throw new ForbidException("You can not update comment you do not own");
        _mapper.Map(request, comment);


        await _commentsRepository.SaveChanges();
    }
}