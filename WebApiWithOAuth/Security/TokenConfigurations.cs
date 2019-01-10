using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace WebApiWithOAuth.Security
{
    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }

        public TokenConfigurations(IConfiguration config)
        {
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                config.GetSection("TokenConfigurations"))
                .Configure(this);
        }
    }
}
