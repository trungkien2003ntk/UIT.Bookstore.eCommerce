using KKBookstore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;


namespace KKBookstore.Infrastructure.Data.Extensions;

internal static class ConfigurationExtensions
{
    public static void ConfigureAuditing(this EntityTypeBuilder b)
    {
        b.TryConfigureExtendedAudited();
        b.TryConfigureExtendedFullAudited();
    }

    private static void TryConfigureExtendedAudited(this EntityTypeBuilder b)
    {
        b.Property<int?>(nameof(IAuditedObject.CreatorId)).HasColumnName(nameof(IAuditedObject.CreatorId));
        b.Property<DateTimeOffset?>(nameof(IAuditedObject.CreationTime)).HasColumnName(nameof(IAuditedObject.CreationTime));
        b.HasOne(nameof(IAuditedObject.Creator)).WithMany().HasForeignKey(nameof(IAuditedObject.CreatorId)).OnDelete(DeleteBehavior.NoAction);

        b.Property<int?>(nameof(IAuditedObject.LastModifierId)).HasColumnName(nameof(IAuditedObject.LastModifierId));
        b.Property<DateTimeOffset?>(nameof(IAuditedObject.LastModificationTime)).HasColumnName(nameof(IAuditedObject.LastModificationTime));
        b.HasOne(nameof(IAuditedObject.LastModifier)).WithMany().HasForeignKey(nameof(IAuditedObject.LastModifierId)).OnDelete(DeleteBehavior.NoAction);
    }

    private static void TryConfigureExtendedFullAudited(this EntityTypeBuilder b)
    {
        var entityType = b.Metadata;
        if (entityType.ClrType.IsAssignableTo<IFullAuditedObject>())
        {
            // b.HasQueryFilter to filter out the deleted entities
            var filterExpression = ConvertFilterExpression<IFullAuditedObject>(e => !e.IsDeleted, entityType.ClrType);
            b.HasQueryFilter(filterExpression);

            b.Property<int?>(nameof(IFullAuditedObject.DeleterId)).HasColumnName(nameof(IFullAuditedObject.DeleterId));
            b.Property<DateTimeOffset?>(nameof(IFullAuditedObject.DeletionTime)).HasColumnName(nameof(IFullAuditedObject.DeletionTime));
            b.Property<bool>(nameof(IFullAuditedObject.IsDeleted)).HasColumnName(nameof(IFullAuditedObject.IsDeleted)).IsRequired();
            b.HasOne(nameof(IFullAuditedObject.Deleter)).WithMany().HasForeignKey(nameof(IFullAuditedObject.DeleterId)).OnDelete(DeleteBehavior.NoAction);
        }
    }

    private static LambdaExpression ConvertFilterExpression<TInterface>(
                            Expression<Func<TInterface, bool>> filterExpression,
                            Type entityType)
    {
        var newParam = Expression.Parameter(entityType);
        var newBody = ReplacingExpressionVisitor.Replace(filterExpression.Parameters.Single(), newParam, filterExpression.Body);

        return Expression.Lambda(newBody, newParam);
    }

    public static bool IsAssignableTo<TTarget>(this Type type)
    {
        return type is null ? throw new ArgumentNullException(nameof(type)) : type.IsAssignableTo(typeof(TTarget));
    }
}