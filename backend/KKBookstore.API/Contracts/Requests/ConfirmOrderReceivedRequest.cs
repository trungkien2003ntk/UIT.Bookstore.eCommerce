namespace KKBookstore.Contracts.Requests;

public class ConfirmOrderReceivedRequest
{
    public string? FeedbackNotes { get; set; }
    public int? Rating { get; set; } // 1-5 stars
}
