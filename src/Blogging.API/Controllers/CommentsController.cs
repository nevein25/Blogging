using Blogging.Application.Comments.Commands.CreateComment;
using Blogging.Application.Comments.Commands.DeleteCommet;
using Blogging.Application.Comments.Commands.UpdateComment;
using Blogging.Application.Comments.DTOs;
using Blogging.Application.Comments.Queries.GetAllCommentsByPostId;
using Blogging.Application.Comments.Queries.GetCommentById;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogging.API.Controllers;

[Route("api/posts/{postId}/comments")]
[ApiController]
[Authorize]
public class CommentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CommentDto?>> GetById(int id)
    {
        var comment = await _mediator.Send(new GetCommentByIdQuery(id));
        return Ok(comment);
    }

    [HttpGet]

    public async Task<ActionResult<CommentDto?>> GetAllCommentByPostId(int postId)
    {
        var comment = await _mediator.Send(new GetAllCommentsByPostIdQuery(postId));
        return Ok(comment);
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment([FromRoute] int postId, CreateCommentCommand command)
    {
        command.PostId = postId;
        var commentId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { postId, Id = commentId }, null);

    }


    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateComment(int id, UpdateCommentCommand command)
    {

        command.Id = id;
        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletComment(int id)
    {
        await _mediator.Send(new DeleteCommentCommand(id));
        return NoContent();
    }


}