using Microsoft.AspNetCore.Components;
using Quiz.Client.Services;
using Quiz.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Client.Pages.Administration.Challenge
{
    public partial class Edit
    {
        [Parameter]
        public Guid ChallengeId { get; set; }
        public ChallengeDto ChallengeVM = new ChallengeDto() {  };
        private List<Quiz.Shared.CategoryDto> categories = new List<Quiz.Shared.CategoryDto>();
        [Inject]
        public ICategoryService CategoryService { get; set; }
        [Inject]
        public IChallengeService ChallengeService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public bool ShowError { get; set; }
        public string Error { get; set; }

        public async Task OnSubmit()
        {
            ShowError = false;

                var result = await ChallengeService.Edit(ChallengeVM);
                if (!result.IsSuccessful)
                {
                    Error = result.ErrorMessage;
                    ShowError = true;
                }
                else
                {
                    NavigationManager.NavigateTo("/administration/challenges");
                }

        }

        protected override async Task OnInitializedAsync()
        {
            var result = await CategoryService.Get();
            categories = result;
            var resultChallenge = await ChallengeService.Get(ChallengeId);
            ChallengeVM = resultChallenge;
            await base.OnInitializedAsync();
        }



    }
}
