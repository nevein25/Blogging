namespace Blogging.Domain.Entities;
public class UserFollow
{
    public int FollowerId { get; set; }
    public User Follower { get; set; } = default!;

    public int FolloweeId { get; set; }
    public User Followee { get; set; } = default!;
}
