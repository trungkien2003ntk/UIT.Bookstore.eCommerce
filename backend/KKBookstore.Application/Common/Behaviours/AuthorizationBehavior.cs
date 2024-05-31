using MediatR;
using System.Reflection;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Common.Security;
using KKBookstore.Application.Common.Exceptions;

namespace KKBookstore.Application.Common.Behaviours;

internal class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ICurrentUser _currentUser;
    private readonly IIdentityService _identityService;

    public AuthorizationBehavior(
        ICurrentUser currentUser,
        IIdentityService identityService)
    {
        _currentUser = currentUser;
        _identityService = identityService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if (authorizeAttributes.Any())
        {
            // Must be authenticated user
            if (_currentUser.Id == null)
            {
                throw new UnauthorizedAccessException();
            }

            // Role-based authorization
            var authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));

            if (authorizeAttributesWithRoles.Any())
            {
                var authorized = false;

                foreach (var roles in authorizeAttributesWithRoles.Select(a => a.Roles.Split(',')))
                {
                    foreach (var role in roles)
                    {
                        var isInRole = await _identityService.IsInRoleAsync(_currentUser.Id, role.Trim());
                        if (isInRole)
                        {
                            authorized = true;
                            break;
                        }
                    }
                }

                // Must be a member of at least one role in roles
                if (!authorized)
                {
                    throw new ForbiddenAccessException();
                }
            }
        }

        // User is authorized / authorization not required
        return await next();
    }
}

