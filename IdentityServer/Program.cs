using GlobalDomain.Infrastructure.RoleSystem;
using GlobalDomain.Infrastructure.Swagger;
using GlobalDomain.Models.Exceptions;
using GlobalDomain.Models.Options;
using IdentityServer.Config;
using IdentityServer.Data;
using IdentityServer.Models.Options;
using IdentityServer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
var identityConfig = configuration
    .GetSection(nameof(IdentityServerOptions))
    .Get<IdentityServerOptions>() ?? throw new ConfigurationException(typeof(IdentityServerOptions));

//DI config

//Identity Server
services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityServerDbContext>()
    .AddDefaultTokenProviders()
    .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<IdentityUser, IdentityRole>>();

services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddInMemoryApiScopes(IdentityConfig.ApiScopes)
    .AddInMemoryIdentityResources(IdentityConfig.IdentityResources)
    .AddInMemoryApiResources(IdentityConfig.ApiResources)
    .AddInMemoryClients(IdentityConfig.Clients)
    .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
    .AddJwtBearerClientAuthentication()
    .AddDeveloperSigningCredential(); //todo: not something we want to use in a production environment

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGenWithBearerAuth("Identity Server API");

//Identity
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = identityConfig.Url;
        options.Audience = identityConfig.Audience;
    });

services.AddAuthorization(options => { options.AddRoleSystemPolicies(); });

//Services
services.AddScoped<IAdminService, AdminService>();
services.AddScoped<IIdentityUserService, IdentityUserService>();
services.AddScoped<IEMailService, EMailService>();

//Identity server services
services.AddScoped<RoleSystemSeeder>();
services.AddScoped<TestUsersSeeder>();
services.AddScoped<ResourceOwnerPasswordValidator>();

//Options
services.Configure<SmtpOptions>(configuration.GetSection(nameof(SmtpOptions)));

//Middlewares
services.AddScoped<SwaggerUiAuthorizationCorrectorMiddleware>();

//Database
services.AddDbContext<IdentityServerDbContext>(optionsBuilder => { optionsBuilder.UseNpgsql(configuration.GetConnectionString("identityDb")); });

var app = builder.Build();

//Data seeders
app.UseSeeder<RoleSystemSeeder>();
app.UseSeeder<TestUsersSeeder>();

//Middlewares config
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUiAuthorizationCorrector();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseIdentityServer();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();