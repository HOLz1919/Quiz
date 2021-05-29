using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Quiz.Client.Services;
using Quiz.Shared.ViewModels;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Client.Pages
{
    public partial class Challenges
    {
        [Inject]
        public IChallengeService ChallengeService { get; set; }
        public List<ChallengeUserView> challenges = new List<ChallengeUserView>();
        [Inject]
        public DialogService DialogService { get; set; }

        [Inject]
        public ILocalStorageService _localStorage { get; set; }
        private string UserId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            UserId = await _localStorage.GetItemAsync<string>("UserId");
            challenges = await ChallengeService.GetChallengeUser(UserId);
            await base.OnInitializedAsync();
        }

        public async Task EndChallenge(Guid challengeId)
        {
            var result = await ChallengeService.EndChallenge(new UserChallengeVM() {ChallengeId=challengeId, ApplicationUserId=UserId, Status=2 });
            if (result.IsSuccessful)
            {
               await ShowInlineDialog(result.Money.ToString());
            }
            else
            {
                await ShowInlineDialogFail();
            }
            await InvokeAsync(StateHasChanged);
        }




        





    }
}
