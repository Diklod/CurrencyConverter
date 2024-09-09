using System.Security.Cryptography.Xml;
using System.Security.Cryptography;
using System.Text;

namespace CurrencyConversion.Global
{
    public class Security
    {
        public string encryptKey = "X&7j$Fq*9^L@z2&v!mR#b8W+P$3JkY6tQ$g*V1d+oC9l4Z^R@N7u!B0x%fM";
        public string EncryptTripleDES(string text)
        {
            string result = string.Empty;

            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            byte[] keyBytes = Encoding.UTF8.GetBytes(encryptKey);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] temp = sha256.ComputeHash(keyBytes);
                byte[] key = new byte[24];
                Array.Copy(temp, key, 24);

                using (TripleDES tripleDES = TripleDES.Create())
                {
                    tripleDES.Key = key;
                    tripleDES.Mode = CipherMode.ECB;
                    tripleDES.Padding = PaddingMode.PKCS7;

                    ICryptoTransform cTransform = tripleDES.CreateEncryptor();

                    byte[] resultBytes = cTransform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                    result = Convert.ToBase64String(resultBytes);
                }
            }

            return result;
        }

        public string GenerateToken()
        {
            string result = string.Empty;

            Guid guid = Guid.NewGuid();
            result = guid.ToString();

            return result;
        }
    }
}
