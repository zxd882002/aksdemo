using System.Security.Cryptography;
using System.Text;

namespace WeatherForecastAPI.Infrastructure.Entensions
{
    public static class StringEncryptExtensions
    {
        public static byte[] GetSHA256(this string inputString)
        {
            using HashAlgorithm algorithm = SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetSHA256String(this string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetSHA256(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}
