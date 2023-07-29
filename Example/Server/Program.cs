using Example.Server.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using ProtoBuf.Grpc.Server;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddCodeFirstGrpc(config =>
{
    config.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal;
    config.EnableDetailedErrors = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(config =>
    {
        config.Cookie.Name = "Example";
        config.Cookie.HttpOnly = true;
        config.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return Task.CompletedTask;
        };
        config.Events.OnRedirectToAccessDenied = context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return Task.CompletedTask;
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseGrpcWeb(new GrpcWebOptions() { DefaultEnabled = true });

app.MapGrpcService<AuthService>();
app.MapGrpcService<WeatherForecastService>();

app.MapRazorPages();
app.MapFallbackToFile("index.html");

app.Run();
