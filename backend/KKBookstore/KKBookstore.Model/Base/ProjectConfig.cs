namespace KKBookstore.Model.Base
{
    public class ProjectConfig
    {
        public static string? AppName { get; set; }

        public static string? NumberFormat { get; set; }

        public static string? DateTimeSqlFormat { get; set; }

        public static string? DateExpSqlFormat { get; set; }

        public static string? DateFormat { get; set; }
        
        public static string? TimeFormat { get; set; }
        
        public static string? DateTimeFormat { get; set; }

        public static string? SpecialCharacters { get; set; }

        public static string? EmailFormat { get; set; }

        public static string? ConnectionString { get; set; }

        public static string? DefaultPassword { get; set; }

        public static int NumberGeneratePassword { get; set; }
        
        public static string? APIKey { get; set; }

        public static string? APISecret { get; set; }

        public static string? EmailFolder { get; set; }

        public static string? Secret { get; set; }

        public static int ExpiresDate { get; set; }

        public static int TimeForExpireRegisterLink { get; set; }

        public static int TimeForExpireLoginLink { get; set; }

        public static string? ClientId { get; set; }

        public static string? ClientSecret { get; set; }

        public static string? RefreshToken { get; set; }

        public static string? Profile { get; set; }

        public static string? Region { get; set; }

        public static string? BaseUrlAPI { get; set; }

        public static string? APIKeyResendEmail { get; set; }
    }
}
