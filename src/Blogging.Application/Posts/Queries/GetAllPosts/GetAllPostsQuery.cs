using Blogging.Application.Common;
using Blogging.Application.Posts.DTOs;
using Blogging.Domain.Constants;
using MediatR;

namespace Blogging.Application.Posts.Queries.GetAllPosts;
public class GetAllPostsQuery : IRequest<PagedResult<PostDto>>
{
    public string? SearchPhraseTitle { get; set; }
    public string? SearchPhraseAuthor { get; set; }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}
