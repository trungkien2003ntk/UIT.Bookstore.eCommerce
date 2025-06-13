// filepath: d:\Github\Bookstore-ECommerce\backend\KKBookstore.Application\Features\Customers\UpdateCustomer\UpdateCustomerCommand.cs
using KKBookstore.Models;
using MediatR;

namespace KKBookstore.Features.Customers.UpdateCustomer;

// Placeholder class - will be implemented later
public record UpdateCustomerCommand : IRequest<Result<int>>
{
    public int Id { get; init; }

    // Properties will be added later
}
