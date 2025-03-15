﻿using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Users;

public static class UserErrors
{
    public static readonly Error NotFound = Error.NotFound("User.NotFound", "User was not found.");
    public static readonly Error CreateFailed = Error.Failure("User.CreateFailed", "User creation failed.");
    public static readonly Error AssignRoleFailed = Error.Failure("User.AssignRoleFailed", "Assigning role to user failed.");
    public static readonly Error AlreadyExists = Error.Conflict("User.AlreadyExists", "User already exists.");
    public static readonly Error InvalidRole = Error.Validation("User.InvalidRole", "An invalid role was provided.");
    public static readonly Error MissingRole = Error.Validation("User.MissingRole", "A role for this user was not provided.");
    public static readonly Error AuthenticatedUserMismatchError = Error.Validation("User.AuthenticatedUserMismatchError", "Authenticated user does not match the account associated with this email.");
    public static readonly Error InvalidCredentials = Error.Unauthorized("User.InvalidCredentials", "Invalid credentials were provided.");
    public static readonly Error UpdateFailed = Error.Failure("User.UpdateFailed", "User update failed.");
    public static readonly Error Unknown = Error.Failure("User.Unknown", "An unknown error occurred.");
    public static readonly Error ShippingAddressNotFound = Error.NotFound("User.ShippingAddressNotFound", "Shipping address was not found.");
    public static readonly Error DeleteShippingAddressFailed = Error.Failure("User.DeleteShippingAddressFailed", "Deleting shipping address failed.");
    public static readonly Error UpdateShippingAddressFailed = Error.Failure("User.UpdateShippingAddressFailed", "Updating shipping address failed.");
}
