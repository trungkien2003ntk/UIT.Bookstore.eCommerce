namespace KKBookstore.Features.Admin.BulkEvaluateRatings;

public class BulkEvaluateRatingsResponse
{
    public int TotalRatings { get; set; }
    public int ProcessedRatings { get; set; }
    public int HiddenRatings { get; set; }
    public int ErrorCount { get; set; }
    public bool DryRun { get; set; }
    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
    
    public string Summary => DryRun 
        ? $"Dry run completed: {ProcessedRatings} ratings would be processed, {HiddenRatings} would be hidden, {ErrorCount} errors"
        : $"Bulk evaluation completed: {ProcessedRatings} ratings processed, {HiddenRatings} hidden, {ErrorCount} errors";
}