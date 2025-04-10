using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Shared.Users;
using MediatR;

namespace KKBookstore.Application.Features.Users.UpdateUserPartial;

public record UpdateUserPartialCommand : IRequest<Result>
{
    public int Id { get; init; }
    public string? ImageUrl { get; init; }
    public DateTimeOffset DateOfBirth { get; init; }
    public Gender Gender { get; init; }
    public string FirstName { get; internal set; }
    public string LastName { get; internal set; }
    public string FullName { get; init; }
    public string Email { get; init; }
    public string? PhoneNumber { get; init; }
}

public class UpdateUserPartialCommandHandler(
    IIdentityService identityService
) : IRequestHandler<UpdateUserPartialCommand, Result>
{
    public async Task<Result> Handle(UpdateUserPartialCommand request, CancellationToken cancellationToken)
    {
        // preprocess some props
        UserHelper.ConvertFullNameToFirstAndLastName(request.FullName, out var firstName, out var lastName);
        request.FirstName = firstName;
        request.LastName = lastName;

        var updateResult = await identityService.UpdateUserPartialAsync(request);

        if (updateResult.IsFailure)
        {
            return Result.Failure(updateResult.Error);
        }

        return updateResult;
    }
}
