using System.IO;

namespace Paynova.Api.Client.Security
{
    public class EhnHeaderDigest : DigestBase
    {
        public EhnHeaderDigest(string secretKey) : base(secretKey)
        {
        }

        public virtual string Calculate(Stream requestContent)
        {
            string content;

            using (var reader = new StreamReader(requestContent))
                content = reader.ReadToEnd();

            return Calculate(content);
        }

        public virtual string Calculate(string requestContent)
        {
            if (string.IsNullOrEmpty(requestContent))
                return null;

            return GenerateSha1String(requestContent + SecretKey);
        }
    }
}