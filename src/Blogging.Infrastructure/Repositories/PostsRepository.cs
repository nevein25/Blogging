using Blogging.Domain.Constants;
using Blogging.Domain.Entities;
using Blogging.Domain.Repositories;
using Blogging.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Blogging.Infrastructure.Repositories;
internal class PostsRepository : IPostsRepository
{
    private readonly BloggingDbContext _context;

    public PostsRepository(BloggingDbContext context)
    {
        _context = context;
    }

    public async Task<int> Create(Post post)
    {
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return post.Id;
    }

    public async Task<IEnumerable<Post>> GetAll()
    {
        return await _context.Posts.Include(p => p.User).ToListAsync();
    }

    public async Task<IEnumerable<Post>> GetFolloweePosts(IEnumerable<int> followeeIds)
    {
        return await _context.Posts
            .Include(p => p.User)
            .Where(p => followeeIds.Contains(p.UserId))
            .ToListAsync();
    }

    public async Task<(IEnumerable<Post>, int)> GetFolloweePostsMatching(IEnumerable<int> followeeIds,
                                                                         string? searchPhraseTitle,
                                                                         string? searchPhraseAuthor,
                                                                         int pageSize,
                                                                         int pageNumber,
                                                                         string? sortBy,
                                                                         SortDirection sortDirection)
    {
        var searchPhraseTitleUpper = searchPhraseTitle?.ToUpper();
        var searchPhraseAuthorUpper = searchPhraseAuthor?.ToUpper();

        var baseQuery = _context.Posts
                              .Include(p => p.User)
                              .Where(p => followeeIds.Contains(p.UserId) &&
                                    (searchPhraseTitleUpper == null || p.Title.ToUpper().Contains(searchPhraseTitleUpper)) &&
                                    (searchPhraseAuthorUpper == null || p.User.NormalizedUserName.Contains(searchPhraseAuthorUpper)));


       // var baseQuery2 = baseQuery.ToList();




        var totalCount = await baseQuery.CountAsync();

        if (sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Post, object>>>
            {
                { nameof(Post.CreatedAt), p => p.CreatedAt },
                { nameof(Post.Title), p => p.Title }
            };

            var selectedColumn = columnsSelector[sortBy];

            baseQuery = sortDirection == SortDirection.Ascending
                                    ? baseQuery.OrderBy(selectedColumn)
                                    : baseQuery.OrderByDescending(selectedColumn);
        }

        var posts = await baseQuery
                                .Skip(pageSize * (pageNumber - 1))
                                .Take(pageSize)
                                .ToListAsync();

        return (posts, totalCount);
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        var post = await _context.Posts
            .Include(p => p.User)
            .FirstOrDefaultAsync(x => x.Id == id);

        return post;
    }

    public async Task Delete(Post entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }
    public Task SaveChanges() => _context.SaveChangesAsync();

}
