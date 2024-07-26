using Microsoft.AspNetCore.Identity;

namespace Blogging.Domain.Entities;
public class User : IdentityUser<int>
{

    public ICollection<Comment> Comments { get; } = new List<Comment>();
    public ICollection<Post> Posts { get; } = new List<Post>();

    public ICollection<UserFollow> Followers { get; set; } = new List<UserFollow>();
    public ICollection<UserFollow> FollowedUsers { get; set; } = new List<UserFollow>();

}
