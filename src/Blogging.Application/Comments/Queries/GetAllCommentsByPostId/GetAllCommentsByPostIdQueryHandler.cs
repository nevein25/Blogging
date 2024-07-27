using AutoMapper;
using Blogging.Application.Comments.DTOs;
using Blogging.Application.Posts.DTOs;
using Blogging.Application.Posts.Queries.GetAllPosts;
using Blogging.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blogging.Application.Comments.Queries.GetAllCommentsByPostId;
internal class GetAllCommentsByPostIdQueryHandler : IRequestHandler<GetAllCommentsByPostIdQuery, IEnumerable<CommentDto>>
{
    private readonly ICommentsRepository _commentsRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllCommentsByPostIdQueryHandler> _logger;

    public GetAllCommentsByPostIdQueryHandler(ICommentsRepository commentsRepository,
                                   IMapper mapper,
                                   ILogger<GetAllCommentsByPostIdQueryHandler> logger)
    {
        _commentsRepository = commentsRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<CommentDto>> Handle(GetAllCommentsByPostIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all comments");

        var comments = await _commentsRepository.GetAllCommentsByPostId(request.Id);
        var commentsDto = _mapper.Map<IEnumerable<CommentDto>>(comments);

        return commentsDto;
    }
}
