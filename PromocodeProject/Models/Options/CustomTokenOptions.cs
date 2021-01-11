namespace PromoCodeProject.Models.Options
{
    public class CustomTokenOptions : ICustomTokenOptions
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public bool RequireHttpsMetadata { get; set; }
    }
}