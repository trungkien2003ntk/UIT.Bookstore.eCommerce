using KKBookstore.Domain.Models;
using MediatR;

namespace KKBookstore.Application.Features.Users.ChangePassword;

public record ChangePasswordCommand(string Email, string CurrentPassword, string NewPassword) : IRequest<Result>;
