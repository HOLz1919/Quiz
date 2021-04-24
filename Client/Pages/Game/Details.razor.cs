using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Quiz.Client.Services;
using Quiz.Shared.Dictionaries;
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
        private bool HideTimer { get; set; } = true;
        private int Counter { get; set; } = 15;
        public HubConnection Connection { get; set; }
        private static System.Timers.Timer aTimer { get; set; }



        protected override async Task OnInitializedAsync()
        {
            var result = await GameService.Get(MatchId);
            Match = result;
            UserId = await _localStorage.GetItemAsync<string>("UserId");
           
            HideElement = IsPossibleToStartMatch();
            await ConnectToServer();
            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            CheckMatchStatus();
            await base.OnAfterRenderAsync(firstRender);
        }

        private bool IsPossibleToStartMatch()
        {
            if (Guid.Parse(UserId) == Match.OwnerId && Match.CountOfPlayers>=2)
            {
                return false;
            }
            return true;
        }

        private async Task StartMatch()
        {
            await Connection.InvokeAsync("StartMatch", Match.Id);
        }


        private async Task ConnectToServer()
        {
            Connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44352/SingleTableHub")
                .Build();

            await Connection.StartAsync();

            Connection.Closed += async (s) =>
            {
                await Connection.StartAsync();
            };

            await Connection.InvokeAsync("JoinGroup", Match.Id);


            Connection.On<MatchView>("UpdateMatch", m =>
            {
                Match = m;
                HideElement=IsPossibleToStartMatch();
                StateHasChanged();
            });

            Connection.On<string>("StartMatch", message =>
            {
                HideTimer = false;
                StartTimer();
                StateHasChanged();
            });

        }

        public void StartTimer()
        {
            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += CountDownTimer;
            aTimer.Enabled = true;
        }

        public void CountDownTimer(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (Counter > 0)
            {
                Counter -= 1;
            }
            else
            {
                aTimer.Enabled = false;
                NavigationManager.NavigateTo("/game/LiveGame/" + MatchId);
            }
            InvokeAsync(StateHasChanged);
        }



        private void CheckMatchStatus()
        {
            if (Match.Status==(int)MatchStatus.ACTIVE)
            {
                NavigationManager.NavigateTo("/game/LiveGame/" + Match.Id);
            }

            if (Match.Status == (int)MatchStatus.ENDED)
            {
                NavigationManager.NavigateTo("/game/gameresults/" + Match.Id);
            }

        }


    }
}
