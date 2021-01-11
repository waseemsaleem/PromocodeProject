using System.Security.Claims;

namespace PromoCodeProject.Common
{
    public interface IJwtManager
    {
        string GenerateToken(string username);
        string GenerateAppToken(string username, string secKey);
        ClaimsPrincipal GetPrincipal(string token);
        ClaimsPrincipal GetAppPrincipal(string token);
        string EncryptText(string plainText, string encryptionPrivateKey = "");
        string DecryptText(string cipherText, string encryptionPrivateKey = "");
        string ToSHA256(string value);
        string GenerateRandomNo();
    }
}