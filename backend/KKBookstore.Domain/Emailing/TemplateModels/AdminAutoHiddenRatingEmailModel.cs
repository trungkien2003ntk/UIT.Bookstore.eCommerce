namespace KKBookstore.Emailing.TemplateModels;

public class AdminAutoHiddenRatingEmailModel : IEmailModel
{
    public string TemplateName => EmailConsts.AdminAutoHiddenRatingEmailTemplateName;

    public string Subject => EmailConsts.AdminAutoHiddenRatingEmailSubject + RatingId;

    public string? ReceiverFullName { get; set; }

    public int RatingId { get; }
    public string Comment { get; }
    public int AiScore { get; }
    public object TemplateDataModel => new
    {
        RecipientName = ReceiverFullName ?? "Admin",
        RatingId = RatingId,
        Comment = Comment,
        AiScore = AiScore
    };

    public AdminAutoHiddenRatingEmailModel(int ratingId, string comment, int aiScore, string? receiverFullName = null)
    {
        RatingId = ratingId;
        Comment = comment;
        AiScore = aiScore;
        ReceiverFullName = receiverFullName;
    }
}
