namespace Blogging.Application.Posts.DTOs;
public class PostDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;

    public DateTime CreatedAt { get; set; } 

    public int UserId { get; set; }
    public string Username { get; set; } = default!;

}
//public record PostsDto(int Id, string Title, string Content, DateTime CreatedAt, int UserId, string Username);
