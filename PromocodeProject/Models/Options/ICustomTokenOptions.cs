namespace PromoCodeProject.Models.Options
{
    public interface ICustomTokenOptions
    {
        string Key { get; set; }
        string Issuer { get; set; }
        string Audience { get; set; }
        bool RequireHttpsMetadata { get; set; }
    }
}