using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using TaskListWebApplication.Helpers;
using TaskListWebApplication.Models.Exceptions;
using TaskListWebApplication.Models.Options;

var builder = WebApplication.CreateBuilder(args);
var serviceCollection = builder.Services;

#region DI

var identityConfig = builder.Configuration
    .GetSection(nameof(IdentityServerOptions))
    .Get<IdentityServerOptions>() ?? throw new ConfigurationException(typeof(IdentityServerOptions));

//Routing
serviceCollection.AddControllers();
serviceCollection.AddEndpointsApiExplorer();

//Swagger
serviceCollection.AddSwaggerGen(options =>
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
serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = identityConfig.Url;
        options.Audience = identityConfig.Audience;
    });

serviceCollection.AddAuthorization(options => { options.AddRoleSystemPolicies(); });

//Middlewares
serviceCollection.AddScoped<SwaggerUiAuthorizationCorrectorMiddleware>();

//Options
serviceCollection.AddOptions<IdentityServerOptions>();

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