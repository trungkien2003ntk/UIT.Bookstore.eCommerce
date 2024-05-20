using KKBookstore.Domain.Common;
using MediatR;

namespace KKBookstore.Application.Users.Commands.VerifyOtp;

public record VerifyOtpCommand(string Email, string Otp) : IRequest<Result<VerifyOtpResponse>>;