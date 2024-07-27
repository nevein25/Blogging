using Blogging.Application.Posts.Commands.CreatePost;
using Blogging.Application.Posts.Commands.DeletePost;
using Blogging.Application.Posts.Commands.UpdatePost;
using Blogging.Application.Posts.DTOs;
using Blogging.Application.Posts.Queries.GetAllPosts;
using Blogging.Application.Posts.Queries.GetPostById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace Blogging.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PostsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostDto?>> GetById(int id)
    {
        var post = await _mediator.Send(new GetPostByIdQuery(id));
        return Ok(post);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var posts = await _mediator.Send(new GetAllPostsQuery());
        return Ok(posts);
    }
    [HttpPost]
    public async Task<IActionResult> CreatePost(CreatePostCommand command)
    {
        var postId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { Id = postId }, null);
        //return Ok();
    }


    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePost(int id, UpdatePostCommand command)
    {

        command.Id = id;
        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletPost(int id)
    {
        await _mediator.Send(new DeletePostCommand(id));
        return NoContent();
    }


}
