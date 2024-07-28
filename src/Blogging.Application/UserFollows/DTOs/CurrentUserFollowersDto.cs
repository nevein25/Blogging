namespace Blogging.Application.UserFollows.DTOs;
public class CurrentUserFollowersDto
{
    public int UserId { get; set; }
    public string Username { get; set; } = default!;
}
