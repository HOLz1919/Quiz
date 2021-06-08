using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Quiz.Client.Services;
using Quiz.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Quiz.Client.Pages
{
    public partial class Statistics
    {

        [Inject]
        public IStatisticsService StatisticsService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public List<StatisticsView> StatisticsViewList = new List<StatisticsView>();
        public List<List<DataItem>> charts = new List<List<DataItem>>() { new List<DataItem>()} ;


        [Inject]
        public ILocalStorageService _localStorage { get; set; }
        private string UserId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            UserId = await _localStorage.GetItemAsync<string>("UserId");
            StatisticsViewList = await StatisticsService.Get(UserId);
            await SetCharts(StatisticsViewList);


            await base.OnInitializedAsync();
        }

        private async Task SetCharts(List<StatisticsView> statisticsViews)
        {
          
            for (int i = 0; i < statisticsViews.Count; i++)
            {
                var loseMatch = new DataItem()
                {
                    Name = "Przegrane Mecze",
                    ValueMatch = statisticsViews[i].MatchCount - statisticsViews[i].WonMatches
                };
                var winMatch = new DataItem()
                {
                    Name = "Wygrane mecze",
                    ValueMatch = statisticsViews[i].WonMatches
                };

                List<DataItem>items =new List<DataItem>() { loseMatch, winMatch};

                charts.Add(items);

            }

        }




        public class DataItem
        {
            public int ValueMatch { get; set; }
            public string Name { get; set; }
        }



    }
}
