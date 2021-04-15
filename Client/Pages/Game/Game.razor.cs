﻿using Blazored.LocalStorage;
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


        void Add()
        {
            NavigationManager.NavigateTo("/game/add");
        }

        protected override async Task OnInitializedAsync()
        {
            var result = await GameService.Get();
            Matches = result;

            UserId = await _localStorage.GetItemAsync<string>("UserId");        
            await base.OnInitializedAsync();
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            IsUserInGame();
            return base.OnAfterRenderAsync(firstRender);
        }

        private void IsUserInGame()
        {
            foreach(var Match in Matches)
            {
                if(Match.Players.Any(item => item.ApplicationUserId == UserId))
                {
                    NavigationManager.NavigateTo("/game/details/" + Match.Id);
                    break;
                }
            }
        }

    }
}