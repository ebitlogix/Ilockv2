using System.Security.Cryptography;
using System.Text;

namespace ILock.Core
{
    /// <summary>
    /// The crypto service.
    /// </summary>
    public class CryptoService : ICryptoService
    {
        /// <summary>
        /// Computes the hash.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>A string.</returns>
        public string ComputeHash(string text)
        {
            var encoding = Encoding.BigEndianUnicode;
            var manage = HashAlgorithm.Create("SHA-256");
            StringBuilder builder = new StringBuilder();
            foreach (byte b in manage!.ComputeHash(Encoding.UTF8.GetBytes(text))) builder.Append(b.ToString("x2").ToUpper());
            return builder.ToString();
        } 
    }
}
