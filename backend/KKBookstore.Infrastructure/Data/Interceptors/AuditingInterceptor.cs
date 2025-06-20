using KKBookstore.Common.Interfaces;
using KKBookstore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace KKBookstore.Data.Interceptors;

public class AuditingInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUser _currentUser;

    public AuditingInterceptor(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;

        if (context is null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);

        context.ChangeTracker.DetectChanges();
        var entries = context.ChangeTracker.Entries();

        foreach (var entry in entries)
        {
            // Common audit for creation and modification
            if (entry.Entity is IAuditedObject auditedEntity)
            {
                if (entry.State == EntityState.Added)
                {
                    auditedEntity.CreatorId = _currentUser.Id;
                    auditedEntity.CreationTime = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    auditedEntity.LastModifierId = _currentUser.Id;
                    auditedEntity.LastModificationTime = DateTime.Now;
                }
            }

            // Additional audit for deletion
            if (entry.Entity is IFullAuditedObject fullAuditedEntity && entry.State == EntityState.Deleted)
            {
                fullAuditedEntity.DeleterId = _currentUser.Id;
                fullAuditedEntity.DeletionTime = DateTime.Now;
                fullAuditedEntity.IsDeleted = true;

                // Optionally set state to Modified to mark as soft-deleted
                entry.State = EntityState.Modified;
            }

            if (entry.State == EntityState.Deleted && entry.Metadata.IsOwned())
            {
                // Find the owner of this owned entity
                var navigation = entry.Metadata.FindOwnership();
                if (navigation != null)
                {
                    var ownerType = navigation.PrincipalEntityType.ClrType;

                    // If the owner supports soft delete, don't delete the owned entity
                    if (typeof(IFullAuditedObject).IsAssignableFrom(ownerType))
                    {
                        entry.State = EntityState.Unchanged;
                    }
                }
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
