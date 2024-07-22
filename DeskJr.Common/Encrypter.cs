using System.Security.Cryptography;
using System.Text;

namespace DeskJr.Common
{
    public class Encrypter
    {
        public static string EncryptString(string stringToBeEncrypted)
        {
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(stringToBeEncrypted);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
