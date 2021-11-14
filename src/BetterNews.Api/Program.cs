using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

WebApplicationBuilder applicationBuilder = WebApplication.CreateBuilder(args);

applicationBuilder.Services.AddControllers();
applicationBuilder.Services.AddEndpointsApiExplorer();
applicationBuilder.Services.AddSwaggerGen();

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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(applicationBuilder.Configuration.GetSection(nameof(SecretsConfiguration)).Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// builder.Services.AddDbContext<AuthenticationContext>();
applicationBuilder.Services.AddScoped<IRoleRepository, RoleRepository>();
applicationBuilder.Services.Configure<SecretsConfiguration>(applicationBuilder.Configuration.GetSection(nameof(SecretsConfiguration)));
applicationBuilder.Services.AddScoped<ITokenServices, TokenServices>();

WebApplication application = applicationBuilder.Build();

application.UseSwagger();
application.UseSwaggerUI();

application.UseHttpsRedirection();

application.UseAuthentication();
application.UseAuthorization();

application.MapControllers();

application.Run();
