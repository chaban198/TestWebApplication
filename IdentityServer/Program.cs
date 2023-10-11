using IdentityServer.Config;
using IdentityServer.Data;
using IdentityServer.Data.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

//DI config
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
    .AddDeveloperSigningCredential(); //todo: not something we want to use in a production environment

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddScoped<IIdentityUserService, IdentityUserService>();
services.AddScoped<RoleSystemSeeder>();
services.AddScoped<ResourceOwnerPasswordValidator>();
services.AddDbContext<IdentityServerDbContext>(optionsBuilder => { optionsBuilder.UseNpgsql(configuration.GetConnectionString("identityDb")); });

var app = builder.Build();

//Middlewares config
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseIdentityServer();

app.UseRouting();
app.MapControllers();

app.UseSeeder<RoleSystemSeeder>();

app.Run();