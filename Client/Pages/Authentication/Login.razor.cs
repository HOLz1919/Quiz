using Microsoft.AspNetCore.Components;
using Quiz.Client.Services;
using Quiz.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Client.Pages.Authentication
{
    public partial class Login
    {
        private LoginModel loginModel = new LoginModel();

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public bool ShowAuthError { get; set; }
        public string Error { get; set; }
        public async Task ExecuteLogin()
        {
            ShowAuthError = false;
            var result = await AuthenticationService.Login(loginModel);
            if (!result.IsAuthSuccessful)
            {
                Error = result.ErrorMessage;
                ShowAuthError = true;
            }
            else
            {
                NavigationManager.NavigateTo("/game");
            }
        }

    }
}
