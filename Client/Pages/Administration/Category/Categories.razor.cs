using Microsoft.AspNetCore.Components;
using Quiz.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Radzen;
using Quiz.Shared;


namespace Quiz.Client.Pages.Administration.Category
{
    public partial class Categories
    {
        [Inject]
        public ICategoryService CategoryService { get; set; }
        public TooltipService tooltipService { get; set; }
        public DialogService DialogService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }


        int count;
        IEnumerable<Quiz.Shared.Category> categories;


        async Task LoadData(LoadDataArgs args)
        {
            categories = await CategoryService.Get();

            if (!string.IsNullOrEmpty(args.Filter))
            {
                categories = categories.AsQueryable().Where(args.Filter).ToList();
            }

            if (!string.IsNullOrEmpty(args.OrderBy))
            {
                categories = categories.AsQueryable().OrderBy(args.OrderBy).ToList();
            }

            categories = categories.Skip(args.Skip.Value).Take(args.Top.Value).ToList();

            count = categories.Count();

            await InvokeAsync(StateHasChanged);
        }

        async Task Edit(Quiz.Shared.Category category)
        {
            await Task.Run(() => NavigationManager.NavigateTo("/administration/users/edit/" + category.Id));
        }

        void Add()
        {
            NavigationManager.NavigateTo("/administration/categories/add");
        }


       

    }
}
