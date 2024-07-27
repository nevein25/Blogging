using Blogging.Domain.Entities;

namespace Blogging.Application.Comments.DTOs;
public class CommentDto
{
    public int Id { get; set; }
    public string Text { get; set; } = default!;

    public DateTime CreatedAt { get; set; } 

    public int UserId { get; set; }
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;

}
