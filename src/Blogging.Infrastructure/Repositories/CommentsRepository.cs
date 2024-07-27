using Blogging.Domain.Entities;
using Blogging.Domain.Repositories;
using Blogging.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Blogging.Infrastructure.Repositories;
internal class CommentsRepository : ICommentsRepository
{
    private readonly BloggingDbContext _context;

    public CommentsRepository(BloggingDbContext context)
    {
        _context = context;
    }

    public async Task<int> Create(Comment comment)
    {
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
        return comment.Id;
    }

    public async Task<IEnumerable<Comment>> GetAll()
    {
        return await _context.Comments.Include(c => c.User).ToListAsync();
    }

    public async Task<IEnumerable<Comment>> GetAllCommentsByPostId(int postId)
    {
        return await _context.Comments.Include(c => c.User)
                        .Where( c=> c.PostId == postId).ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        var comment = await _context.Comments
            .Include(c => c.User)
            .FirstOrDefaultAsync(x => x.Id == id);

        return comment;
    }

    public async Task Delete(Comment entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }
    public Task SaveChanges() => _context.SaveChangesAsync();

}