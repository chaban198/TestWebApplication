using GlobalDomain.Infrastructure.RoleSystem;
using GlobalDomain.Infrastructure.Swagger;
using GlobalDomain.Models.Exceptions;
using GlobalDomain.Models.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using TaskListWebApplication.Data;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

#region DI

var identityConfig = configuration
    .GetSection(nameof(IdentityServerOptions))
    .Get<IdentityServerOptions>() ?? throw new ConfigurationException(typeof(IdentityServerOptions));

//Routing
services.AddControllers();
services.AddEndpointsApiExplorer();

//Swagger
services.AddSwaggerGenWithBearerAuth("Доска Задач API");

//Identity
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = identityConfig.Url;
        options.Audience = identityConfig.Audience;
    });

services.AddAuthorization(options => { options.AddRoleSystemPolicies(); });

//Middlewares
services.AddScoped<SwaggerUiAuthorizationCorrectorMiddleware>();

//Options
services.Configure<IdentityServerOptions>(configuration.GetSection(nameof(IdentityServerOptions)));

//Database
services.AddDbContext<TaskListApplicationDbContext>(optionsBuilder => { optionsBuilder.UseNpgsql(configuration.GetConnectionString("taskListDb")); });
services.AddDbContext<IdentityReadonlyDbContext>(optionsBuilder => { optionsBuilder.UseNpgsql(configuration.GetConnectionString("identityDb")); });

#endregion

#region Middlewares

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUiAuthorizationCorrector();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion