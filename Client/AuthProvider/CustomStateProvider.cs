using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Quiz.Client.AuthProvider
{
    public class CustomStateProvider : AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(1500);
            
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, "John Doe"),
        new Claim(ClaimTypes.Role, "Admin")
    };


            var anonymous = new ClaimsIdentity(claims, "testAuthType");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymous)));
        }
    }
}
