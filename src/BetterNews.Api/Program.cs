using BetterNews.Infrastructure.Data.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

WebApplicationBuilder applicationBuilder = WebApplication.CreateBuilder(args);

applicationBuilder.Services.AddControllers();
applicationBuilder.Services.AddEndpointsApiExplorer();
applicationBuilder.Services.AddSwaggerGen();
applicationBuilder.Services.AddAutoMapper(typeof(Program).Assembly);

ConfigurationManager configurations = applicationBuilder.Configuration;

applicationBuilder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options => {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configurations.GetSection(nameof(SecretsConfiguration)).Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

applicationBuilder.Services.AddHttpContextAccessor();
applicationBuilder.Services.AddSingleton<HttpContextAccessorHelper>();

applicationBuilder.Services.AddDbContext<AuthenticationContext>(options => options.UseSqlServer(configurations.GetConnectionString("Accounts")));
applicationBuilder.Services.AddScoped<IRoleRepository, RoleRepository>();
applicationBuilder.Services.AddScoped<IUserRepository, UserRepository>();
applicationBuilder.Services.AddScoped<IUsersRolesRepository, UsersRolesRepository>();

applicationBuilder.Services.Configure<SecretsConfiguration>(configurations.GetSection(nameof(SecretsConfiguration)));
applicationBuilder.Services.AddScoped<ITokenServices, TokenServices>();

applicationBuilder.Services.AddScoped<SeedingServices>();

WebApplication application = applicationBuilder.Build();

application.UseSwagger();
application.UseSwaggerUI();

application.UseHttpsRedirection();

application.UseAuthentication();
application.UseAuthorization();

application.MapControllers();

await application.CreateRolesAsync();
application.Run();
