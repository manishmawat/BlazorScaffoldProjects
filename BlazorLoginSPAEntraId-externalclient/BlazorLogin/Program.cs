
using BlazorLogin;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var Configuration = builder.Configuration;
var clientId = Configuration["Auth0:ClientId"];

builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
    options.ProviderOptions.ClientId = builder.Configuration["Auth0:ClientId"];
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.DefaultScopes.Add("openid");
    options.ProviderOptions.DefaultScopes.Add("profile");
    options.ProviderOptions.DefaultScopes.Add("email");
    // Optional: options.ProviderOptions.RedirectUri = "<YOUR_REDIRECT_URI>";
});

await builder.Build().RunAsync();
