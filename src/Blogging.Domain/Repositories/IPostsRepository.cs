using Blogging.Domain.Constants;
using Blogging.Domain.Entities;

namespace Blogging.Domain.Repositories;
public interface IPostsRepository
{
    Task<int> Create(Post post);
    Task<IEnumerable<Post>> GetAll();
    Task<IEnumerable<Post>> GetFolloweePosts(IEnumerable<int> followeeIds);
    Task<(IEnumerable<Post>, int)> GetFolloweePostsMatching(IEnumerable<int> followeeIds,
                                                                         string? searchPhraseTitle,
                                                                         string? searchPhraseAuthor,
                                                                         int pageSize,
                                                                         int pageNumber,
                                                                         string? sortBy,
                                                                         SortDirection sortDirection);
    Task<Post?> GetByIdAsync(int id);
    Task Delete(Post entity);
    Task SaveChanges();
}
