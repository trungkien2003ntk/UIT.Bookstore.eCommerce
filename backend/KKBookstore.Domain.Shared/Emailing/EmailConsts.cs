namespace KKBookstore.Emailing;

public static class EmailConsts
{
    public const string BaseNamespace = "KKBookstore.Emailing.Templates.";
    public const string SubjectPrefix = "[KKBooks] "; public const string LayoutTemplateName = "Layout";
    public const string LayoutTemplateAsmNamespace = BaseNamespace + "EmailLayout.sbn";

    #region Authentication
    public const string AccountRegistrationEmailSubject = SubjectPrefix + "Xác nhận đăng ký tài khoản";
    public const string AccountRegistrationEmailTemplateName = "AccountRegistration";
    public const string AccountRegistrationEmailAsmNamespace = BaseNamespace + "AccountRegistration.sbn";

    public const string ForgotPasswordEmailSubject = SubjectPrefix + "Yêu cầu đặt lại mật khẩu";
    public const string ForgotPasswordEmailTemplateName = "ForgotPassword";
    public const string ForgotPasswordEmailAsmNamespace = BaseNamespace + "ForgotPassword.sbn";
    #endregion

    #region Moderation
    public const string AdminAutoHiddenRatingEmailSubject = SubjectPrefix + "Thông báo ẩn đánh giá tự động - Đánh giá #";
    public const string AdminAutoHiddenRatingEmailTemplateName = "AdminAutoHiddenRating";
    public const string AdminAutoHiddenRatingEmailAsmNamespace = BaseNamespace + "AdminAutoHiddenRating.sbn";

    public const string UserHiddenRatingEmailSubject = SubjectPrefix + "Đánh giá của bạn đã bị ẩn";
    public const string UserHiddenRatingEmailTemplateName = "UserHiddenRating";
    public const string UserHiddenRatingEmailAsmNamespace = BaseNamespace + "UserHiddenRating.sbn";

    public const string UserRestoredRatingEmailSubject = SubjectPrefix + "Đánh giá của bạn đã được khôi phục";
    public const string UserRestoredRatingEmailTemplateName = "UserRestoredRating";
    public const string UserRestoredRatingEmailAsmNamespace = BaseNamespace + "UserRestoredRating.sbn";
    #endregion

    #region Orders
    public const string OrderConfirmationEmailSubject = SubjectPrefix + "Xác nhận đơn hàng";
    public const string OrderConfirmationEmailTemplateName = "OrderConfirmation";
    public const string OrderConfirmationEmailAsmNamespace = BaseNamespace + "OrderConfirmation.sbn";
    #endregion


    public const string DefaultContentType = "plain";
    public const string SenderName = "KKBookstore";
    public const string ReceiverName = "KKBookstore Customer";
}
