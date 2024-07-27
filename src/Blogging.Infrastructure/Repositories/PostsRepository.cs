using Blogging.Domain.Entities;
using Blogging.Domain.Repositories;
using Blogging.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

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
        return await _context.Posts.Include( p=> p.User).ToListAsync();
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
