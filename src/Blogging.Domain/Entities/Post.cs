namespace Blogging.Domain.Entities;
public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = default!;
    public int UserId { get; set; }

    public ICollection<Comment> Comments { get; } = new List<Comment>();


}
