using KKBookstore.API.Extensions;
using KKBookstore.Application.Users.Commands.ReplaceUser;
using KKBookstore.Application.Users.Commands.UpdateUser;
using KKBookstore.Application.Users.Queries.GetUser;
using KKBookstore.Application.Users.Queries.GetUserList;
using KKBookstore.Domain.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetUsersAsync(
            [FromQuery] GetUserListQuery query,
            CancellationToken cancellationToken = default
        )
        {
            var result = await mediator.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(
            int id,
            CancellationToken cancellationToken = default
        )
        {
            var result = await mediator.Send(new GetUserQuery(id), cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToActionResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ReplaceUserAsync(
            int id,
            [FromBody] ReplaceUserCommand command,
            CancellationToken cancellationToken = default
        )
        {
            if (command.Id != id)
            {
                return BadRequest();
            }

            var result = await mediator.Send(command, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToActionResult();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUserPartialAsync(
            int id,
            [FromBody] JsonPatchDocument<UpdateUserCommand> patchDoc,
            CancellationToken cancellationToken = default
        )
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var command = new UpdateUserCommand { Id = id };
            patchDoc.ApplyTo(command, (error) => ModelState.AddModelError("", error.ErrorMessage));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await mediator.Send(command, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToActionResult();
        }

    }
}
