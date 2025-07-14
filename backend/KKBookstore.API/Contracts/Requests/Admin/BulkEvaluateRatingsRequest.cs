namespace KKBookstore.API.Contracts.Requests.Admin;

public record BulkEvaluateRatingsRequest(
    bool DryRun = false
);