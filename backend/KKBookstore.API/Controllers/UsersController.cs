
using KKBookstore.API.Abstractions;
using KKBookstore.API.Contracts.Requests;
using KKBookstore.API.Contracts.Requests.Users;
using KKBookstore.Application.Features.Users.AddShippingAddress;
using KKBookstore.Application.Features.Users.DeleteShippingAddress;
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

namespace KKBookstore.API.Controllers;

[Route("api/users")]
[Authorize]
public class UsersController(
    ISender sender
) : ApiController(sender)
{
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetUsersAsync(
        [FromQuery] GetUserListRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var query = new GetUserListQuery
        {
            UserIds = request.UserIds,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            SortBy = request.SortBy,
            SortDirection = request.SortDirection
        };

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

    [Authorize(Roles = $"{Role.Admin},{Role.Customer}")]
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

    [Authorize(Roles = Role.Customer)]
    [HttpGet("{userId}/addresses/get-address-list")]
    public async Task<IActionResult> GetUserShippingAddressesAsync(
        int userId,
        CancellationToken cancellationToken = default
    )
    {
        var userIdFromClaims = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!);
        if (userIdFromClaims != userId)
        {
            return BadRequest();
        }

        var result = await Sender.Send(new GetUserShippingAddressesQuery(userIdFromClaims), cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [Authorize(Roles = Role.Customer)]
    [HttpPost("{userId}/addresses/create-address")]
    public async Task<IActionResult> AddUserShippingAddressAsync(
        int userId,
        [FromBody] AddShippingAddressRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var userIdFromClaims = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!);
        if (userIdFromClaims != userId)
        {
            return BadRequest();
        }


        var command = new AddShippingAddressCommand
        {
            UserId = userIdFromClaims,
            ProvinceName = request.Province,
            DistrictName = request.District,
            CommuneName = request.Commune,
            IsDefault = request.IsDefault,
            Type = request.Type,
            ReceiverName = request.ReceiverName,
            PhoneNumber = request.PhoneNumber,
            DetailAddress = request.DetailAddress
        };

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? CreatedAtAction(nameof(GetUserShippingAddressesAsync), new { userIdFromClaims }, result.Value) : ToActionResult(result);
    }

    [Authorize(Roles = Role.Customer)]
    [HttpPut]
    [Route("{userId}/addresses/{id}")]
    public async Task<IActionResult> UpdateUserShippingAddressAsync(
        int userId,
        int id,
        [FromBody] UpdateShippingAddressCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var userIdFromClaims = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!);

        if (command.UserId != userIdFromClaims || userId != userIdFromClaims || command.Id != id)
        {
            return BadRequest();
        }

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? NoContent() : ToActionResult(result);
    }

    [Authorize(Roles = Role.Customer)]
    [HttpDelete]
    [Route("{userId}/addresses/{addressId}")]
    public async Task<IActionResult> DeleteUserShippingAddressAsync(
        int userId,
        int addressId,
        CancellationToken cancellationToken = default
    )
    {
        var userIdFromClaims = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!);
        if (userIdFromClaims != userId)
        {
            return BadRequest();
        }

        var result = await Sender.Send(new DeleteShippingAddressCommand(addressId), cancellationToken);
        return result.IsSuccess ? NoContent() : ToActionResult(result);
    }
}
