using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public static class ConfigureAuthenticationServices
{
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, ConfigurationManager configurations)
    {
        services.Configure<SecretsConfiguration>(configurations.GetSection(nameof(SecretsConfiguration)));
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.RequireAuthenticatedSignIn = true;
        })
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddJwtBearer(options => 
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configurations
                    .GetSection(nameof(SecretsConfiguration)).Get<SecretsConfiguration>().Secret)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        return services;
    }
}
