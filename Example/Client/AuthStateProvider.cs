using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Example.Client
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _principal = new ClaimsPrincipal(new ClaimsIdentity());

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return await Task.FromResult(new AuthenticationState(_principal));
        }

        public void SetAuth(string username)
        {
            _principal = new ClaimsPrincipal(
                new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, username),
            }, nameof(AuthStateProvider)));

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void ClearAuth()
        {
            _principal = new ClaimsPrincipal(new ClaimsIdentity());

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
