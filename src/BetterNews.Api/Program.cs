WebApplicationBuilder applicationBuilder = WebApplication.CreateBuilder(args);

applicationBuilder.Services.AddControllers();
applicationBuilder.Services.AddEndpointsApiExplorer();
applicationBuilder.Services.AddCors();
applicationBuilder.Services.AddHttpContextAccessor();

applicationBuilder.Services.AddSingleton<HttpContextAccessorHelper>();
applicationBuilder.Services.ConfigureDependencies(applicationBuilder.Configuration);


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