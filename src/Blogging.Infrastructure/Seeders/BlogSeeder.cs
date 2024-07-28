using Blogging.Domain.Entities;
using Blogging.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blogging.Infrastructure.Seeders;
internal class BlogSeeder : IBlogSeeder
{
    private readonly BloggingDbContext _context;
    private readonly UserManager<User> _userManager;

    public BlogSeeder(BloggingDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public async Task Seed()
    {
        if (_context.Database.GetPendingMigrations().Any())
            await _context.Database.MigrateAsync();


        if (await _context.Database.CanConnectAsync())
        {
            if (!_context.Users.Any())
            {
                var users = GetUsers();
                var password = "TEST@test123";
                foreach (var user in users)
                {
                    await _userManager.CreateAsync(user, password);
                }
            }

            if (!_context.UserFollows.Any())
            {
                var users = await _context.Users.ToListAsync();
                var userFollows = GetUserFollows(users);
                await _context.UserFollows.AddRangeAsync(userFollows);
                await _context.SaveChangesAsync();
            }

            if (!_context.Posts.Any())
            {
                var posts = await GetPostsAsync();
                await _context.Posts.AddRangeAsync(posts);
                await _context.SaveChangesAsync();
            }

            if (!_context.Comments.Any())
            {
                var comments = await GetCommentsAsync();
                await _context.Comments.AddRangeAsync(comments);
                await _context.SaveChangesAsync();
            }
        }
    }


    private IList<User> GetUsers()
    {
        var users = new List<User>{
                                    new User {UserName="JohnDoe", Email = "JohnDoe@gmail.com" },
                                    new User {UserName="JaneSmith",Email = "JaneSmith@gmail.com" },
                                    new User {UserName="RobertBrown", Email = "RobertBrown@gmail.com" },
                                    new User {UserName="EmilyDavis", Email = "EmilyDavis@gmail.com" },
                                    new User {UserName="MichaelWilson", Email = "MichaelWilson@gmail.com" },
                                    new User {UserName="EmmaClark", Email = "JohnDoeEmmaClarkgmail.com" },
                                    new User {UserName="JamesLewis", Email = "JamesLewis@gmail.com" },
                                    new User {UserName="OliviaWalker",Email = "OliviaWalker@gmail.com" },
                                    new User {UserName="DavidHall",Email = "DavidHall@gmail.com" }
                                   };
        return users;
    }

    private IList<UserFollow> GetUserFollows(List<User> users)
    {
        var userFollows = new List<UserFollow>();

        if (users.Count >= 2)
        {
            userFollows.Add(new UserFollow { FollowerId = users[0].Id, FolloweeId = users[1].Id });
            userFollows.Add(new UserFollow { FollowerId = users[0].Id, FolloweeId = users[2].Id });
            userFollows.Add(new UserFollow { FollowerId = users[0].Id, FolloweeId = users[3].Id });
            userFollows.Add(new UserFollow { FollowerId = users[0].Id, FolloweeId = users[4].Id });
            userFollows.Add(new UserFollow { FollowerId = users[4].Id, FolloweeId = users[5].Id });
            userFollows.Add(new UserFollow { FollowerId = users[5].Id, FolloweeId = users[6].Id });
            userFollows.Add(new UserFollow { FollowerId = users[6].Id, FolloweeId = users[7].Id });
            userFollows.Add(new UserFollow { FollowerId = users[7].Id, FolloweeId = users[8].Id });
        }

        return userFollows;
    }

    private async Task<IList<Post>> GetPostsAsync()
    {
        var users = await _context.Users.ToDictionaryAsync(u => u.UserName, u => u.Id);

        var posts = new List<Post>
                    {
                        new Post { Title = "Post 1", Content = "Content for post 1", UserId = users["JohnDoe"], CreatedAt = DateTime.UtcNow.AddDays(-9) },
                        new Post { Title = "Post 2", Content = "Content for post 2", UserId = users["JaneSmith"], CreatedAt = DateTime.UtcNow.AddDays(-8) },
                        new Post { Title = "Post 3", Content = "Content for post 3", UserId = users["RobertBrown"], CreatedAt = DateTime.UtcNow.AddDays(-7) },
                        new Post { Title = "Post 4", Content = "Content for post 4", UserId = users["EmilyDavis"], CreatedAt = DateTime.UtcNow.AddDays(-6) },
                        new Post { Title = "Post 5", Content = "Content for post 5", UserId = users["MichaelWilson"], CreatedAt = DateTime.UtcNow.AddDays(-5) },
                        new Post { Title = "Post 6", Content = "Content for post 6", UserId = users["EmmaClark"], CreatedAt = DateTime.UtcNow.AddDays(-4) },
                        new Post { Title = "Post 7", Content = "Content for post 7", UserId = users["JamesLewis"], CreatedAt = DateTime.UtcNow.AddDays(-3) },
                        new Post { Title = "Post 8", Content = "Content for post 8", UserId = users["OliviaWalker"], CreatedAt = DateTime.UtcNow.AddDays(-2) },
                        new Post { Title = "Post 9", Content = "Content for post 9", UserId = users["DavidHall"], CreatedAt = DateTime.UtcNow.AddDays(-1) }
                    };

        return posts;
    }
    private async Task<IList<Comment>> GetCommentsAsync()
    {
        var users = await _context.Users.ToDictionaryAsync(u => u.UserName, u => u.Id);
        var posts = await _context.Posts.ToDictionaryAsync(p => p.Title, p => p.Id);

        var comments = new List<Comment>
            {
                new Comment { Text = "Comment 1 on Post 1", UserId = users["JohnDoe"], PostId = posts["Post 1"] },
                new Comment { Text = "Comment 2 on Post 1", UserId = users["JaneSmith"], PostId = posts["Post 1"] },
                new Comment { Text = "Comment 1 on Post 2", UserId = users["RobertBrown"], PostId = posts["Post 2"] },
                new Comment { Text = "Comment 2 on Post 2", UserId = users["EmilyDavis"], PostId = posts["Post 2"] },
                new Comment { Text = "Comment 1 on Post 3", UserId = users["MichaelWilson"], PostId = posts["Post 3"] },
                new Comment { Text = "Comment 2 on Post 3", UserId = users["EmmaClark"], PostId = posts["Post 3"] },
                new Comment { Text = "Comment 1 on Post 4", UserId = users["JamesLewis"], PostId = posts["Post 4"] },
                new Comment { Text = "Comment 2 on Post 4", UserId = users["OliviaWalker"], PostId = posts["Post 4"] },
                new Comment { Text = "Comment 1 on Post 5", UserId = users["DavidHall"], PostId = posts["Post 5"] }
            };

        return comments;
    }

}