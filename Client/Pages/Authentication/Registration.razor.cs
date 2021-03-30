using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Quiz.Client.Services;
using Quiz.Shared;
using Quiz.Shared.Responses;

namespace Quiz.Client.Pages.Authentication
{
    public partial class Registration
    {
        private RegistrationModel registrationModel = new RegistrationModel();

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public bool ShowRegistrationErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }


        public async Task Register()
        {
            ShowRegistrationErrors = false;
            var result = await AuthenticationService.RegisterUser(registrationModel);
            if (!result.IsSuccessfulRegistration)
            {
                Errors = result.Errors;
                ShowRegistrationErrors = true;
            }
            else
            {
                NavigationManager.NavigateTo("/game");
            }
        }

    }
}
