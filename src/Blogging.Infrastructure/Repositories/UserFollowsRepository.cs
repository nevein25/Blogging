using Blogging.Domain.Entities;
using Blogging.Domain.Repositories;
using Blogging.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Blogging.Infrastructure.Repositories;
internal class UserFollowsRepository : IUserFollowsRepository
{
    private readonly BloggingDbContext _context;

    public UserFollowsRepository(BloggingDbContext context)
    {
        _context = context;
    }

    public async Task<int> Create(UserFollow userFollow)
    {
        _context.UserFollows.Add(userFollow);
        await _context.SaveChangesAsync();
        return userFollow.FolloweeId;
    }

    public async Task<UserFollow?> GetById(int followerId, int followeeId)
    {
        return await _context.UserFollows
            .FirstOrDefaultAsync(uf => uf.FollowerId == followerId && uf.FolloweeId == followeeId);
    }
    public async Task Delete(UserFollow entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsFollowing(int followerId, int followeeId)
    {
        return await _context.UserFollows
                    .AnyAsync(uf => uf.FollowerId == followerId && uf.FolloweeId == followeeId);
    }


    public async Task<IEnumerable<UserFollow>> GetUsersThatCurrentUserIsFollowing(int followerId)
    {
        return await _context.UserFollows
                            .Include(uf => uf.Followee)
                            .Where(uf => uf.FollowerId == followerId)
                            .ToListAsync();
    }
    public Task SaveChanges() => _context.SaveChangesAsync();
}
