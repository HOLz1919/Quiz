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
    public partial class Game
    {

        private List<MatchView> Matches = new List<MatchView>();
        [Inject]
        public IGameService GameService { get; set; }
        [Inject]
        public TooltipService tooltipService { get; set; }
        [Inject]
        public DialogService DialogService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }


        void Add()
        {
            NavigationManager.NavigateTo("/game/add");
        }

        protected override async Task OnInitializedAsync()
        {
            var result = await GameService.Get();
            Matches = result;

            await base.OnInitializedAsync();
        }

        private async Task IsUserInGame()
        {

        }

    }
}
