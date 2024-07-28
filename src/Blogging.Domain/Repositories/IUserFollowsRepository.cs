using Blogging.Domain.Entities;

namespace Blogging.Domain.Repositories;
public interface IUserFollowsRepository
{
    Task<int> Create(UserFollow userFollow);
    Task<UserFollow?> GetById(int followerId, int followeeId);

    Task<IEnumerable<UserFollow>> GetUsersThatCurrentUserIsFollowing(int followerId);

    Task<bool> IsFollowing(int followerId, int followeeId);
    Task Delete(UserFollow entity);
    Task SaveChanges();
}
