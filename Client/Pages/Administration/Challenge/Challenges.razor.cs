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

namespace Quiz.Client.Pages.Administration.Challenge
{
    public partial class Challenges
    {

        [Inject]
        public ICategoryService CategoryService { get; set; }
        [Inject]
        public IChallengeService ChallengeService { get; set; }
        [Inject]
        public TooltipService tooltipService { get; set; }
        [Inject]
        public DialogService DialogService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }


        int count;
        IEnumerable<ChallengeView> challenges;
        RadzenGrid<ChallengeView> challengeGrid;


        async Task LoadData(LoadDataArgs args)
        {
            challenges = await ChallengeService.Get();

            if (!string.IsNullOrEmpty(args.Filter))
            {
                challenges = challenges.AsQueryable().Where(args.Filter).ToList();
            }

            if (!string.IsNullOrEmpty(args.OrderBy))
            {
                challenges = challenges.AsQueryable().OrderBy(args.OrderBy).ToList();
            }

            challenges = challenges.Skip(args.Skip.Value).Take(args.Top.Value).ToList();

            count = challenges.Count();

            await InvokeAsync(StateHasChanged);
        }

        async Task Edit(ChallengeView challenge)
        {
            await Task.Run(() => NavigationManager.NavigateTo("/administration/challenges/edit/" + challenge.Id));
        }

        void Add()
        {
            NavigationManager.NavigateTo("/administration/challenges/add");
        }





    }
}
