using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace KKBookstore.Infrastructure.Data.Interceptors;

public class AuditInterceptor : ISaveChangesInterceptor
{
    // Todo: try to fix the issue with _currnetUserId is null, handle all the possible cases, and go here to remove the need of using a default user id.
    private const int DEFAULT_USER_ID = 1;
    private readonly ICurrentUser _currentUser;

    public AuditInterceptor(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    public ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var currnetUserId = _currentUser?.Id ?? DEFAULT_USER_ID;

        foreach (var entry in eventData.Context!.ChangeTracker.Entries<BaseAuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = currnetUserId;
                    entry.Entity.CreatedWhen = DateTime.UtcNow;
                    entry.Entity.LastEditedBy = currnetUserId;
                    entry.Entity.CreatedWhen = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastEditedBy = currnetUserId;
                    entry.Entity.LastEditedWhen = DateTime.UtcNow;
                    break;
            }
        }

        return new ValueTask<InterceptionResult<int>>(result);
    }

    public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        // This method can be left empty if you only intend to use async saving changes.
        return result;
    }
}
