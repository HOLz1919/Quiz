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
        public QuestionVM Question = new QuestionVM() { Answers = new List<Quiz.Shared.AnswerDto>() };
        private List<Quiz.Shared.CategoryDto> categories = new List<Quiz.Shared.CategoryDto>();
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
            int count = Question.Answers.Where(item => item.IsCorrect == true).Count();
            if (count == 1)
            {
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
            else
            {
                ShowError = true;
                Error = "Musisz wybrać jedną poprawną odpowiedź";
            }

        }

        protected override async Task OnInitializedAsync()
        {
            var result = await CategoryService.Get();
            categories = result;
            var resultQuestion =  await QuestionService.Get(QuestionId);
            Question = resultQuestion;
            await base.OnInitializedAsync();
        }

        private void AddAnswer()
        {
            Question.Answers.Add(new Quiz.Shared.AnswerDto() { Content = null });
        }





    }
}
