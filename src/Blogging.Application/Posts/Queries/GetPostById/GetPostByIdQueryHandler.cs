using AutoMapper;
using Blogging.Application.Posts.DTOs;
using Blogging.Application.Posts.Queries.GetAllPosts;
using Blogging.Domain.Entities;
using Blogging.Domain.Exceptions;
using Blogging.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blogging.Application.Posts.Queries.GetPostById;
public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostDto?>
{
    private readonly IPostsRepository _postsRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetPostByIdQueryHandler> _logger;

    public GetPostByIdQueryHandler(IPostsRepository postsRepository,
                                   IMapper mapper,
                                   ILogger<GetPostByIdQueryHandler> logger)
    {
        _postsRepository = postsRepository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<PostDto?> Handle(GetPostByIdQuery? request, CancellationToken cancellationToken)
    {
        if (request == null) return null;

        _logger.LogInformation($"Getting post {request.Id}");
        var post = await _postsRepository.GetByIdAsync(request.Id) 
                        ?? throw new NotFoundException(nameof(Post), request.Id.ToString()); ;
        var postDto = _mapper.Map<PostDto?>(post);

        return postDto;
    }
}
