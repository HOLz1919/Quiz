using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
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



        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

            Money = await _localStorage.GetItemAsync<int>("money");

            await base.OnAfterRenderAsync(firstRender);
        }
    }
}
