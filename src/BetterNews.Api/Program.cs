using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

WebApplicationBuilder applicationBuilder = WebApplication.CreateBuilder(args);

applicationBuilder.Services.AddControllers();
applicationBuilder.Services.AddEndpointsApiExplorer();
applicationBuilder.Services.AddCors();
applicationBuilder.Services.AddSwaggerGen();
applicationBuilder.Services.AddAutoMapper(typeof(Program).Assembly);

ConfigurationManager configurations = applicationBuilder.Configuration;
applicationBuilder.Services.Configure<SecretsConfiguration>(configurations.GetSection(nameof(SecretsConfiguration)));

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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configurations.GetSection(nameof(SecretsConfiguration)).Get<SecretsConfiguration>().Secret)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

applicationBuilder.Services.AddHttpContextAccessor();
applicationBuilder.Services.AddSingleton<HttpContextAccessorHelper>();

applicationBuilder.Services.AddDbContext<AuthenticationContext>(options => options.UseSqlServer(configurations.GetConnectionString("Accounts")));
applicationBuilder.Services.AddScoped<IRoleRepository, RoleRepository>();

applicationBuilder.Services.AddScoped<IUserRepository, UserRepository>();
applicationBuilder.Services.AddScoped<IUserServices, UserServices>();

applicationBuilder.Services.AddScoped<IUsersRolesRepository, UsersRolesRepository>();

applicationBuilder.Services.AddScoped<CrossCuttingRepository>();

applicationBuilder.Services.AddScoped<ITokenServices, TokenServices>();

applicationBuilder.Services.AddScoped<SeedingServices>();

WebApplication application = applicationBuilder.Build();

application.UseSwagger();
application.UseSwaggerUI();

application.UseHttpsRedirection();
application.UseCors(setup => setup.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

application.UseAuthentication();
application.UseAuthorization();

await application.CreateRolesAsync();

application.MapControllers();
application.Run();