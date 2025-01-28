using System.Security.Cryptography;
using System.Text;

namespace CAPTAR.Services
{
    public class UtilitisService
    {
        public static string ConvertSHA256(string text)
        {
            string hash = string.Empty;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                foreach (byte b in hashValue) hash += $"{b:X2}";
            }
            return hash;
        }

        public static string GenerateToken()
        {
            string token = Guid.NewGuid().ToString("N");
            return token;
        }

    }
}
