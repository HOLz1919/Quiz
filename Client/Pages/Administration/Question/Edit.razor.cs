using Microsoft.AspNetCore.Components;
using Quiz.Client.Services;
using Quiz.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Client.Pages.Administration.Question
{
    public partial class Edit
    {
        [Parameter]
        public Guid QuestionId { get; set; }
        public QuestionVM Question = new QuestionVM();
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
            var result = await QuestionService.Edit(Question);
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
            var result =  await QuestionService.Get(QuestionId);
            Question = result;
            await base.OnInitializedAsync();
        }





    }
}
