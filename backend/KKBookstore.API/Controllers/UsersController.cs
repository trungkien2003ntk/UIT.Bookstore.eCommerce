
using KKBookstore.API.Abstractions;
using KKBookstore.Application.Features.Users.AddShippingAddress;
using KKBookstore.Application.Features.Users.GetUser;
using KKBookstore.Application.Features.Users.GetUserList;
using KKBookstore.Application.Features.Users.GetUserShippingAddresses;
using KKBookstore.Application.Features.Users.ReplaceUser;
using KKBookstore.Application.Features.Users.UpdateShippingAddress;
using KKBookstore.Application.Features.Users.UpdateUser;
using KKBookstore.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KKBookstore.API.Controllers
{
    [Route("api/users")]
    [Authorize]
    public class UsersController(
        ISender sender
    ) : ApiController(sender)
    {
        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetUsersAsync(
            [FromQuery] GetUserListRequest query,
            CancellationToken cancellationToken = default
        )
        {
            var result = await Sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(
            int id,
            CancellationToken cancellationToken = default
        )
        {
            var result = await Sender.Send(new GetUserQuery(id), cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
        }

        [Authorize(Roles=$"{Role.Admin},{Role.Customer}")]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUserAsync(CancellationToken cancellationToken = default)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return Unauthorized();
            }

            var result = await Sender.Send(new GetUserQuery(int.Parse(userId)), cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
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

            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? NoContent() : ToActionResult(result);
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

            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? NoContent() : ToActionResult(result);
        }


        [HttpGet("{userId}/shipping-addresses")]
        public async Task<IActionResult> GetUserShippingAddressesAsync(
            int userId,
            CancellationToken cancellationToken = default
        )
        {
            var result = await Sender.Send(new GetUserShippingAddressesRequest(userId), cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
        }

        [HttpPost("{userId}/shipping-addresses")]
        public async Task<IActionResult> AddUserShippingAddressAsync(
            int userId,
            [FromBody] AddShippingAddressCommand command,
            CancellationToken cancellationToken = default
        )
        {
            if (command.UserId != userId)
            {
                return BadRequest();
            }

            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? CreatedAtAction(nameof(GetUserShippingAddressesAsync), new { userId }, result.Value) : ToActionResult(result);
        }

        [HttpPut("{userId}/shipping-addresses/{id}")]
        public async Task<IActionResult> UpdateUserShippingAddressAsync(
            int userId,
            int id,
            [FromBody] UpdateShippingAddressCommand command,
            CancellationToken cancellationToken = default
        )
        {
            if (command.UserId != userId || command.Id != id)
            {
                return BadRequest();
            }

            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? NoContent() : ToActionResult(result);
        }
    }
}
