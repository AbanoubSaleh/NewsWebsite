using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants.Permissions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .Build();

// Add API controllers and OpenIddict services
builder.Services.AddControllers();
builder.Services.AddOpenIddict()
    .AddServer(options =>
    {
        options.SetTokenEndpointUris("/connect/token");
        options.AllowClientCredentialsFlow();
        options.AddEphemeralEncryptionKey()
               .AddEphemeralSigningKey();
        options.RegisterScopes(Scopes.Email, Scopes.Profile);
        options.UseAspNetCore()
               .EnableTokenEndpointPassthrough();
    });

WebApplication app = builder.Build();

await app.BootUmbracoAsync();

app.UseHttpsRedirection();

// Use authentication & authorization
app.UseAuthentication();
app.UseAuthorization();

// Map controllers using attribute routing
app.MapControllers();

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseInstallerEndpoints();
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();