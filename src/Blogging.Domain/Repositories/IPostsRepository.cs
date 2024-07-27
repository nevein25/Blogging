using Blogging.Domain.Entities;

namespace Blogging.Domain.Repositories;
public interface IPostsRepository
{
    Task<int> Create(Post post);
    Task<IEnumerable<Post>> GetAll();
    Task<Post?> GetByIdAsync(int id);
    Task Delete(Post entity);
    Task SaveChanges();
}
