using AutoMapper;
using Blogging.Application.Comments.DTOs;
using Blogging.Application.Posts.DTOs;
using Blogging.Application.Posts.Queries.GetPostById;
using Blogging.Domain.Entities;
using Blogging.Domain.Exceptions;
using Blogging.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blogging.Application.Comments.Queries.GetCommentById;
public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, CommentDto?>
{
    private readonly ICommentsRepository _commentsRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCommentByIdQueryHandler> _logger;

    public GetCommentByIdQueryHandler(ICommentsRepository commentsRepository,
                                   IMapper mapper,
                                   ILogger<GetCommentByIdQueryHandler> logger)
    {
        _commentsRepository = commentsRepository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<CommentDto?> Handle(GetCommentByIdQuery? request, CancellationToken cancellationToken)
    {
        if (request == null) return null;

        _logger.LogInformation($"Getting comment {request.Id}");
        var comment = await _commentsRepository.GetByIdAsync(request.Id)
                        ?? throw new NotFoundException(nameof(Comment), request.Id.ToString()); ;
        var commentDto = _mapper.Map<CommentDto?>(comment);

        return commentDto;
    }
}
