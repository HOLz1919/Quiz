using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Quiz.Client.Services;
using Quiz.Shared.ViewModels;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
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
        public NotifierService Notifier { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public ILocalStorageService _localStorage { get; set; }

        int count;
        IEnumerable<UserMatchView> scores;
        RadzenGrid<UserMatchView> grid;


        async Task LoadData(LoadDataArgs args)
        {
            scores = Scores.AsEnumerable();

            if (!string.IsNullOrEmpty(args.Filter))
            {
                scores = scores.AsQueryable().Where(args.Filter).ToList();
            }

            if (!string.IsNullOrEmpty(args.OrderBy))
            {
                scores = scores.AsQueryable().OrderBy(args.OrderBy).ToList();
            }

            scores = scores.Skip(args.Skip.Value).Take(args.Top.Value).ToList();

            count = scores.Count();

            await InvokeAsync(StateHasChanged);
        }



        protected override async Task OnInitializedAsync()
        {

            UserId = await _localStorage.GetItemAsync<string>("UserId");
            Scores = await GameService.GetResults(MatchId);
            await GameService.EndMatch(MatchId);
            await grid.Reload();
            var result = await GameService.GetUserMoney(UserId);
            if (result.IsSuccessful)
            {
                await _localStorage.SetItemAsync("money", result.Money);
                await Notifier.AddTolist("0");
            }
            await base.OnInitializedAsync();

        }



    }
}
