using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Quiz.Client.Services;
using Quiz.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Client.Pages.Game
{
    public partial class Add
    {
        private List<Quiz.Shared.CategoryDto> categories = new List<Quiz.Shared.CategoryDto>();
        private MatchDto Match = new MatchDto();
        [Inject]
        public ICategoryService CategoryService { get; set; }
        [Inject]
        public IGameService GameService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public ILocalStorageService _localStorage { get; set; }
        [Inject]
        public NotifierService Notifier { get; set; }

        public int Money { get; set; } = 0;

        public bool ShowError { get; set; }
        public string Error { get; set; }
        public async Task OnSubmit()
        {
            ShowError = false;
            if (Money > Match.Bid)
            {
                var result = await GameService.Add(Match);
                if (!result.IsSuccessful)
                {
                    Error = result.ErrorMessage;
                    ShowError = true;
                }
                else
                {
                    await _localStorage.SetItemAsync("money", Money - Match.Bid);
                    await Notifier.AddTolist((Money-Match.Bid).ToString());
                    NavigationManager.NavigateTo("/game/details/" + result.MatchId);
                }
            }
            else
            {
                ShowError = true;
                Error = "Masz za mało pieniędzy";
            }
            
        }


        protected override async Task OnInitializedAsync()
        {
            var result = await CategoryService.Get();
            categories = result;
            if (categories != null && categories.Count() > 0) Match.CategoryId = categories[0].Id;
            Match.MaxCountOfPlayers = 10;
            Match.Status = 1;
            Money = await _localStorage.GetItemAsync<int>("money");
            var userId= await _localStorage.GetItemAsync<string>("UserId");
            Match.OwnerId = Guid.Parse(userId);
            await base.OnInitializedAsync();
        }

     

        //private void AddAnswer()
        //{
        //    QuestionVM.Answers.Add(new Quiz.Shared.AnswerDto() { Content = null });
        //}
    }
}
