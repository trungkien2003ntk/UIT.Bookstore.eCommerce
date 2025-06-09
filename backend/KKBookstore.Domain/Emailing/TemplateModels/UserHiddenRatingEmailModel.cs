namespace KKBookstore.Emailing.TemplateModels;

public class UserHiddenRatingEmailModel : IEmailModel
{
    public string TemplateName => EmailConsts.UserHiddenRatingEmailTemplateName;

    public string Subject => EmailConsts.UserHiddenRatingEmailSubject;

    public string? ReceiverFullName { get; set; }

    public int RatingId { get; }
    public string Reason { get; }

    public object TemplateDataModel => new
    {
        recipient_name = ReceiverFullName ?? "Khách hàng",
        rating_id = RatingId,
        reason = Reason
    };

    public UserHiddenRatingEmailModel(int ratingId, string reason, string? receiverFullName = null)
    {
        RatingId = ratingId;
        Reason = reason;
        ReceiverFullName = receiverFullName;
    }
}
