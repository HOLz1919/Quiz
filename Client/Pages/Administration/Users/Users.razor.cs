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

namespace Quiz.Client.Pages.Administration.Users
{
    public partial class Users
    {

        [Inject]
        public IUserService UserService { get; set; }
        [Inject]
        public TooltipService tooltipService { get; set; }
        [Inject]
        public DialogService DialogService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }


        int count;
        IEnumerable<UserVM> users;
        RadzenGrid<UserVM> usersGrid;


        async Task LoadData(LoadDataArgs args)
        {
            users = await UserService.Get();

            if (!string.IsNullOrEmpty(args.Filter))
            {
                users = users.AsQueryable().Where(args.Filter).ToList();
            }

            if (!string.IsNullOrEmpty(args.OrderBy))
            {
                users = users.AsQueryable().OrderBy(args.OrderBy).ToList();
            }

            users = users.Skip(args.Skip.Value).Take(args.Top.Value).ToList();

            count = users.Count();

            await InvokeAsync(StateHasChanged);
        }

        async Task Edit(UserVM user)
        {
            await Task.Run(() => NavigationManager.NavigateTo("/administration/users/edit/" + user.Id));
        }

        void Add()
        {
            NavigationManager.NavigateTo("/administration/users/add");
        }




    }
}
