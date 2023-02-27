using System.Security.Cryptography;
using System.Text;

namespace Preezie.Application.Extensions
{
    public static class StringExtensions
    {
        public static string EncryptPassword(this string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
