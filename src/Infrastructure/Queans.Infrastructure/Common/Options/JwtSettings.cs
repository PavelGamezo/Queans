namespace Queans.Infrastructure.Common.Options
{
    public class JwtSettings
    {
        public const string SectionName = "JwtSettings";

        public string Secret { get; set; } = null!;

        public string Issuer { get; set; } = null!;
        
        public int ExpiredTimeInMinutes { get; set; } = 0;
        
        public string Audience { get; set; } = null!;
    }
}
