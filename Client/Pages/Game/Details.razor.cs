using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Quiz.Client.Services;
using Quiz.Shared.ViewModels;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Client.Pages.Game
{
    public partial class Details
    {
        public MatchView Match = new MatchView() { Players = new List<UserMatchView>()};
        [Parameter]
        public Guid MatchId { get; set; }
        private string UserId { get; set; }
        [Inject]
        public IGameService GameService { get; set; }
        [Inject]
        public TooltipService tooltipService { get; set; }
        [Inject]
        public DialogService DialogService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public ILocalStorageService _localStorage { get; set; }
        private bool HideElement { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            var result = await GameService.Get(MatchId);
            Match = result;
            UserId = await _localStorage.GetItemAsync<string>("UserId");
            HideElement = IsPossibleToStartMatch();
            await base.OnInitializedAsync();
        }

        private bool IsPossibleToStartMatch()
        {
            if (Guid.Parse(UserId) == Match.OwnerId && Match.CountOfPlayers>=2)
            {
                return false;
            }
            return true;
        }


    }
}
