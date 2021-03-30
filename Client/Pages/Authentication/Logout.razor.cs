using Microsoft.AspNetCore.Components;
using Quiz.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Client.Pages.Authentication
{
    public partial class Logout
    {
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await AuthenticationService.Logout();
            NavigationManager.NavigateTo("/");
        }


    }
}
