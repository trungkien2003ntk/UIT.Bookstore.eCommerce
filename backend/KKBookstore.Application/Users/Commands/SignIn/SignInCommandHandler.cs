using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Common;
using KKBookstore.Domain.Users;
using MediatR;

namespace KKBookstore.Application.Users.Commands.SignIn;

public class SignInCommandHandler : IRequestHandler<SignInCommand, Result<SignInResponse>>
{
    private readonly IIdentityService _identityService;

    public SignInCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<SignInResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.SignInAsync(request.ToDto());
    }
}