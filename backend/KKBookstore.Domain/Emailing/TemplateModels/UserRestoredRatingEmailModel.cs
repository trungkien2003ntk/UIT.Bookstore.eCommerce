namespace KKBookstore.Emailing.TemplateModels;

public class UserRestoredRatingEmailModel : IEmailModel
{
    public string TemplateName => EmailConsts.UserRestoredRatingEmailTemplateName;

    public string Subject => EmailConsts.UserRestoredRatingEmailSubject;

    public string? ReceiverFullName { get; set; }

    public int RatingId { get; }

    public object TemplateDataModel => new
    {
        recipient_name = ReceiverFullName ?? "Khách hàng",
        rating_id = RatingId
    };

    public UserRestoredRatingEmailModel(int ratingId, string? receiverFullName = null)
    {
        RatingId = ratingId;
        ReceiverFullName = receiverFullName;
    }
}
