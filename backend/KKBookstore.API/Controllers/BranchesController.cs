using AutoMapper;
using KKBookstore.Abstractions;
using KKBookstore.Features.Branches.CreateBranch;
using KKBookstore.Features.Branches.DeleteBranch;
using KKBookstore.Features.Branches.GetBranchDetail;
using KKBookstore.Features.Branches.GetBranchList;
using KKBookstore.Features.Branches.UpdateBranch;
using KKBookstore.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.Controllers;

[Route("api/branches")]
public class BranchesController(
    ISender sender,
    IMapper mapper
) : ApiController(sender)
{
    [HttpGet]
    public async Task<IActionResult> GetBranches(
        [FromQuery] GetBranchListQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBranchDetail(
        [FromRoute] int id,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(new GetBranchDetailQuery(id), cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBranch(
        [FromBody] CreateBranchCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBranch(
        [FromRoute] int id,
        [FromBody] UpdateBranchCommand command,
        CancellationToken cancellationToken = default
    )
    {
        if (id != command.Id)
        {
            var resultTemp = Result.Failure(Error.Validation("Endpoint.InvalidRequest", "Branch id in request doesn't match with the id in the route"));
            return ToActionResult(resultTemp);
        }

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBranch(
        [FromRoute] int id,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(new DeleteBranchCommand(id), cancellationToken);

        return result.IsSuccess ? Ok() : ToActionResult(result);
    }
}