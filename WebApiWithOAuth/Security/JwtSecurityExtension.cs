using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiWithOAuth.Security
{
    public static class JwtSecurityExtension
    {
        public static IServiceCollection AddJwtSecurity(
            this IServiceCollection services,
            SigningConfigurations signConfigurations,
            TokenConfigurations tokenConfigurations)
        {
            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signConfigurations.Key;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy(Roles.ROLE_API_ADMIN, new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .RequireRole(Roles.ROLE_API_ADMIN)
                    .Build());

                auth.AddPolicy(Roles.ROLE_API_NORMAL, new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .RequireRole(Roles.ROLE_API_NORMAL)
                    .Build());
            });
            return services;
        }
    }
}
