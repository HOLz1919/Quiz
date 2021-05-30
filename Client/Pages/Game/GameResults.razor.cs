using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Quiz.Client.Services;
using Quiz.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Client.Pages.Game
{
    public partial class GameResults
    {
        [Parameter]
        public Guid MatchId { get; set; }


        public List<UserMatchView> Scores = new List<UserMatchView>();
        private string UserId { get; set; }


        [Inject]
        public IGameService GameService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public ILocalStorageService _localStorage { get; set; }



        protected override async Task OnInitializedAsync()
        {

            UserId = await _localStorage.GetItemAsync<string>("UserId");
            Scores = await GameService.GetResults(MatchId);
            await base.OnInitializedAsync();
        }



    }
}
