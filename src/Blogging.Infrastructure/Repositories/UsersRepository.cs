using Blogging.Domain.Repositories;
using Blogging.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;


namespace Blogging.Infrastructure.Repositories;
internal class UsersRepository : IUsersRepository
{
    private readonly BloggingDbContext _context;

    public UsersRepository(BloggingDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsUserExistById(int id) => await _context.Users.AnyAsync(u => u.Id == id);

}
