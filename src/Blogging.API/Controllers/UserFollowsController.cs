using Blogging.Application.Comments.DTOs;
using Blogging.Application.Comments.Queries.GetAllCommentsByPostId;
using Blogging.Application.Posts.Commands.DeletePost;
using Blogging.Application.UserFollows.Commands.FollowUser;
using Blogging.Application.UserFollows.Commands.UnFollowUser;
using Blogging.Application.UserFollows.Queries.GetAllFollowers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blogging.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserFollowsController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserFollowsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> FollowUser(CreateUserFollowCommand command)
    {
        var follow = await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete("{followeeId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UnFollow(int followeeId)
    {
        await _mediator.Send(new DeleteUserFollowCommand(followeeId));
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<CommentDto?>> GetUserImFollowing()
    {
        var followig = await _mediator.Send(new GetAllCurrentUserFollowersQuery());
        return Ok(followig);
    }
}
