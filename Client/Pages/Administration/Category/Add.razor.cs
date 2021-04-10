using Microsoft.AspNetCore.Components;
using Quiz.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Client.Pages.Administration.Category
{
    public partial class Add
    {
        private Quiz.Shared.CategoryDto Category = new Quiz.Shared.CategoryDto();

        [Inject]
        public ICategoryService CategoryService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public bool ShowError { get; set; }
        public string Error { get; set; }
        public async Task OnSubmit()
        {
            ShowError = false;
            var result = await CategoryService.Add(Category);
            if (!result.IsSuccessful)
            {
                Error = result.ErrorMessage;
                ShowError = true;
            }
            else
            {
                NavigationManager.NavigateTo("/administration/categories");
            }
        }

    }
}
