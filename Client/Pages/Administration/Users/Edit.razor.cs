using Microsoft.AspNetCore.Components;
using Quiz.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Client.Pages.Administration.Users
{
    public partial class Edit
    {

        [Parameter]
        public string UserId { get; set; }
        public Quiz.Shared.ViewModels.UserVM User = new Quiz.Shared.ViewModels.UserVM();
        [Inject]
        public IUserService UserService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public bool ShowError { get; set; }
        public string Error { get; set; }

        public async Task OnSubmit()
        {
            ShowError = false;
            var result = await UserService.Edit(User);
            if (!result.IsSuccessful)
            {
                Error = result.ErrorMessage;
                ShowError = true;
            }
            else
            {
                NavigationManager.NavigateTo("/administration/users");
            }
          
            
        }

        protected override async Task OnInitializedAsync()
        {
            var result = await UserService.Get(UserId);
            User = result;
            await base.OnInitializedAsync();
        }








    }
}
