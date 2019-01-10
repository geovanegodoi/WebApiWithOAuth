using System;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace WebApiWithOAuth.Security
{
    public class SigningConfigurations
    {
        public SecurityKey Key { get; }
        public SigningCredentials Credentials { get; }

        public SigningConfigurations()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }
            Credentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}
