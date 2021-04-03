using Microsoft.AspNetCore.Components;
using Quiz.Client.Services;
using Quiz.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Client.Pages.Administration.Question
{
    public partial class Add
    {
        private QuestionVM QuestionVM = new QuestionVM();
        private List<Quiz.Shared.Category> categories = new List<Quiz.Shared.Category>(); 
        [Inject]
        public ICategoryService CategoryService { get; set; }
        [Inject]
        public IQuestionService QuestionService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public bool ShowError { get; set; }
        public string Error { get; set; }
        public async Task OnSubmit()
        {
            ShowError = false;
            var result = await QuestionService.Add(QuestionVM);
            if (!result.IsSuccessful)
            {
                Error = result.ErrorMessage;
                ShowError = true;
            }
            else
            {
                NavigationManager.NavigateTo("/administration/questions");
            }
        }


        protected override async Task OnInitializedAsync()
        {
            var result = await CategoryService.Get();
            categories = result;
            await base.OnInitializedAsync();
        }


    }
}
