using AutoMapper;
using Blogging.Application.Posts.DTOs;
using Blogging.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blogging.Application.Posts.Queries.GetAllPosts;
public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, IEnumerable<PostDto>>
{
    private readonly IPostsRepository _postsRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllPostsQueryHandler> _logger;

    public GetAllPostsQueryHandler(IPostsRepository postsRepository,
                                   IMapper mapper,
                                   ILogger<GetAllPostsQueryHandler> logger)
    {
        _postsRepository = postsRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<PostDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all posts");

        var posts = await _postsRepository.GetAll();
        var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
       
        return postsDto;
    }
}
