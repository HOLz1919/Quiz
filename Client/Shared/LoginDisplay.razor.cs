using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Quiz.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Client.Shared
{
    public partial class LoginDisplay
    {
        [Inject]
        public ILocalStorageService _localStorage { get; set; }
        public int Money { get; set; } = 0;

        [Inject]
        public NotifierService Notifier { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Money = await _localStorage.GetItemAsync<int>("money");
            Notifier.Notify += OnNotify;
            await  base.OnInitializedAsync();
        }

        public async Task UpdateMoney()
        {
            Money = await _localStorage.GetItemAsync<int>("money");
            await InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            Notifier.Notify -= OnNotify;
        }

        public async Task OnNotify()
        {
            Money = await _localStorage.GetItemAsync<int>("money");
            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }

    }
}
