using IdentityServer.Config;
using Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

//DI config
services.AddIdentityServer()
    .AddInMemoryApiScopes(IdentityConfig.ApiScopes)
    .AddInMemoryClients(IdentityConfig.Clients)
    .AddInMemoryIdentityResources(IdentityConfig.IdentityResources)
    .AddInMemoryApiResources(IdentityConfig.ApiResources)
    .AddTestUsers(IdentityConfig.TestUsers) //todo: not something we want to use in a production environment
    .AddProfileService<RoleClaimSetterService>()
    .AddDeveloperSigningCredential(); //todo: not something we want to use in a production environment

services.AddScoped<RoleClaimSetterService>();

var app = builder.Build();

//Middlewares config
app.UseHttpsRedirection();
app.UseIdentityServer();

app.Run();