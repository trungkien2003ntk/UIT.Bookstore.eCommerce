namespace KKBookstore.Domain.Shared.Emailing;

public static class EmailConsts
{
    public const string BaseNamespace = "KKBookstore.Domain.Emailing.Templates.";
    public const string SubjectPrefix = "[KKBooks] ";

    public const string LayoutTemplateName = "Layout";
    public const string LayoutTemplateAsmNamespace = BaseNamespace + "EmailLayout.sbn";

    #region Authentication
    public const string AccountRegistrationEmailSubject = SubjectPrefix + "Xác nhận đăng ký tài khoản";
    public const string AccountRegistrationEmailTemplateName = "AccountRegistration";
    public const string AccountRegistrationEmailAsmNamespace = BaseNamespace + "AccountRegistration.sbn";
    #endregion


    #region Orders

    #endregion


    public const string DefaultContentType = "plain";
    public const string SenderName = "KKBookstore";
    public const string ReceiverName = "KKBookstore Customer";
}
