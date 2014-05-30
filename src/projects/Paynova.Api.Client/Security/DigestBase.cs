using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Paynova.Api.Client.EnsureThat;

namespace Paynova.Api.Client.Security
{
    public abstract class DigestBase
    {
        protected readonly string SecretKey;

        protected DigestBase(string secretKey)
        {
            Ensure.That(secretKey, "secretKey").IsNotNullOrEmpty();

            SecretKey = secretKey;
        }

        protected virtual string GenerateSha1String(string rawData)
        {
            var bytes = GenerateSha1Bytes(rawData);
            if (bytes == null || !bytes.Any())
                return null;

            return BitConverter.ToString(bytes).Replace("-", string.Empty).ToUpper();
        }

        protected virtual byte[] GenerateSha1Bytes(string rawData)
        {
            var bytes = Encoding.UTF8.GetBytes(rawData);

            using (var crypt = SHA1.Create())
                return crypt.ComputeHash(bytes);
        }
    }
}