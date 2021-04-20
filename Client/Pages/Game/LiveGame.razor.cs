using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Quiz.Client.Services;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Client.Pages.Game
{
    public partial class LiveGame
    {
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

        public HubConnection Connection { get; set; }


        protected override async Task OnInitializedAsync()
        {

            UserId = await _localStorage.GetItemAsync<string>("UserId");
            await ConnectToServer();
            await  base.OnInitializedAsync();
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

            await Connection.InvokeAsync("JoinGroup", MatchId);



        }



    }
}
