using Blogging.Domain.Entities;

namespace Blogging.Domain.Repositories;
public interface ICommentsRepository
{
    Task<int> Create(Comment Comment);
    Task<IEnumerable<Comment>> GetAll();
    Task<IEnumerable<Comment>> GetAllCommentsByPostId(int postId);
    Task<Comment?> GetByIdAsync(int id);
    Task Delete(Comment entity);
    Task SaveChanges();
}
