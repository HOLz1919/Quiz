using Microsoft.AspNetCore.Components;
using Quiz.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Client.Pages.Administration.Category
{
    public partial class Edit
    {
        [Parameter]
        public Guid CategoryId { get; set; }
        public Quiz.Shared.CategoryDto Category = new Quiz.Shared.CategoryDto();
        [Inject]
        public ICategoryService CategoryService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public bool ShowError { get; set; }
        public string Error { get; set; }

        public async Task OnSubmit()
        {
            ShowError = false;
            var result = await CategoryService.Edit(Category);
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

        protected override async Task OnInitializedAsync()
        {
            var result =  await CategoryService.Get(CategoryId);
            Category = result;
            await base.OnInitializedAsync();
        }





    }
}
