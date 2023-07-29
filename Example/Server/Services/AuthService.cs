using Example.Shared.Authentication;
using Grpc.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using ProtoBuf.Grpc;
using System.Security.Claims;

namespace Example.Server.Services
{
    public class AuthService : IAuthService
    {
        public async Task SignIn(string username, CallContext context)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            identity.AddClaim(new Claim(ClaimTypes.Name, username));

            var principal = new ClaimsPrincipal(identity);

            var httpContext = context.ServerCallContext!.GetHttpContext();

            await httpContext.SignInAsync(principal, new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = true
            });
        }

        public async Task SignOut(CallContext context)
        {
            var httpContext = context.ServerCallContext!.GetHttpContext();

            await httpContext.SignOutAsync();
        }

        public async Task<AuthIdentity> GetIdentity(CallContext context = default)
        {
            var httpContext = context.ServerCallContext!.GetHttpContext();

            var identity = httpContext.User.Identity!;

            if (identity.IsAuthenticated)
            {
                return await Task.FromResult(new AuthIdentity
                {
                    IsAuthenticated = true,
                    Username = identity.Name!
                });
            }

            return new AuthIdentity
            {
                IsAuthenticated = false
            };
        }
    }
}
