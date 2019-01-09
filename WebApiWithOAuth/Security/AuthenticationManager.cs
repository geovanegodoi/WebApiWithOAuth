using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace WebApiWithOAuth.Security
{
    public class AuthenticationManager
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;

        public AuthenticationManager(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }

        public bool ValidateCredentials(UserCredentials credentials)
        {
            bool isValidCredentials = false;

            if (credentials != null && !String.IsNullOrWhiteSpace(credentials.UserID))
            {
                var userIdentity = _userManager.FindByNameAsync(credentials.UserID).Result;
                if (userIdentity != null)
                {
                    var login = _signInManager.CheckPasswordSignInAsync(userIdentity, credentials.Password, false).Result;
                    if (login.Succeeded)
                    {
                        isValidCredentials = (_userManager.IsInRoleAsync(userIdentity, Roles.ROLE_API_ADMIN).Result ||
                                              _userManager.IsInRoleAsync(userIdentity, Roles.ROLE_API_NORMAL).Result);
                    }
                }
            }
            return isValidCredentials;
        }

        public Token GenerateToken(UserCredentials crendentials)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(crendentials.UserID, "Login"),
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, crendentials.UserID)
                });
            var dtCreated = DateTime.Now;
            var dtExpire = dtCreated + TimeSpan.FromSeconds(1000);
            var handler = new JwtSecurityTokenHandler();
            var secutiryToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = "http://localhost:5001",
                Audience = "http://localhost:5001",
                SigningCredentials = null,
                Subject = identity,
                NotBefore = dtCreated,
                Expires = dtExpire
            });
            var token = handler.WriteToken(secutiryToken);

            return new Token
            {
                Authenticated = true,
                Created = dtCreated.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = dtExpire.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                Message = "OK"
            };
        }
    }

    public class UserCredentials
    {
        public string UserID { get; set; }

        public string Password { get; set; }
    }

    public class Token
    {
        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
        public string Message { get; set; }
    }
}
