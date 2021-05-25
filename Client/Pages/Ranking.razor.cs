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

namespace Quiz.Client.Pages
{
    public partial class Ranking
    {

        [Inject]
        public IRankingService RankingService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }


        int count;
        IEnumerable<ResultsView> results;
        RadzenGrid<ResultsView> resultsGrid;


        async Task LoadData(LoadDataArgs args)
        {
            results = await RankingService.Get();

            if (!string.IsNullOrEmpty(args.Filter))
            {
                results = results.AsQueryable().Where(args.Filter).ToList();
            }

            if (!string.IsNullOrEmpty(args.OrderBy))
            {
                results = results.AsQueryable().OrderBy(args.OrderBy).ToList();
            }

            results = results.Skip(args.Skip.Value).Take(args.Top.Value).ToList();

            count = results.Count();

            await InvokeAsync(StateHasChanged);
        }




    }
}
