using Example.Client;
using Example.Shared.Authentication;
using Example.Shared.WeatherForecast;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using ProtoBuf.Grpc.ClientFactory;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var address = new Uri(builder.HostEnvironment.BaseAddress);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddTransient(provider => new GrpcWebHandler(GrpcWebMode.GrpcWeb, new CredentialHandler(new HttpClientHandler())));

builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(p => p.GetRequiredService<AuthStateProvider>());

builder.Services.AddAuthorizationCore();

builder.Services
    .AddCodeFirstGrpcClient<IAuthService>((provider, options) => { options.Address = address; })
    .ConfigurePrimaryHttpMessageHandler<GrpcWebHandler>();

builder.Services
    .AddCodeFirstGrpcClient<IWeatherForecastService>((provider, options) => { options.Address = address; })
    .ConfigurePrimaryHttpMessageHandler<GrpcWebHandler>();

await builder.Build().RunAsync();

public class CredentialHandler : DelegatingHandler
{
    public CredentialHandler(HttpMessageHandler inner) : base(inner) { }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        return base.SendAsync(request, cancellationToken);
    }
}