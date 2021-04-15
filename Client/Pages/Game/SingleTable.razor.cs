using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Quiz.Client.Services;
using Quiz.Shared.ViewModels;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Client.Pages.Game
{
    public partial class SingleTable
    {
        [Parameter]
        public MatchView Match { get; set; }
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

        RadzenButton joinButton;

        protected override async Task OnInitializedAsync()
        {
           UserId = await _localStorage.GetItemAsync<string>("UserId");
           await base.OnInitializedAsync();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (Match.CountOfPlayers >= Match.MaxCountOfPlayers)
            {
                joinButton.Disabled = true;
            }
            base.OnAfterRender(firstRender);
        }

        public async Task Join()
        {
            var result = await GameService.Join(Match.Id, UserId);
            if (result.IsSuccessful)
            {
                NavigationManager.NavigateTo("/game/details/" + Match.Id);
            }
    
        }
    }
}
