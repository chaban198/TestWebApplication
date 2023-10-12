using GlobalDomain.Models.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using TaskListWebApplication.Helpers;
using TaskListWebApplication.Models.Options;

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
services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Доска Задач API", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "JWT by Identity Server 4",
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

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