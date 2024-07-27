using AutoMapper;
using Blogging.Application.Users;
using Blogging.Domain.Entities;
using Blogging.Domain.Exceptions;
using Blogging.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blogging.Application.Comments.Commands.DeleteCommet;
internal class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
{
    private readonly ICommentsRepository _commentsRepository;
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;
    private readonly ILogger<DeleteCommentCommandHandler> _logger;

    public DeleteCommentCommandHandler(ICommentsRepository commentsRepository,
                                    IMapper mapper,
                                    IUserContext userContext,
                                    ILogger<DeleteCommentCommandHandler> logger)
    {
        _commentsRepository = commentsRepository;
        _mapper = mapper;
        _userContext = userContext;
        _logger = logger;
    }

    public async Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting comment with id: {CommentId}", request.Id);
        var comment = await _commentsRepository.GetByIdAsync(request.Id);
        if (comment is null) throw new NotFoundException(nameof(Comment), request.Id.ToString());

        int? loggedInUserId = _userContext.UserId;
        if (loggedInUserId != comment.UserId) throw new ForbidException("You can not delete comment you do not own");

        await _commentsRepository.Delete(comment);
    }
}