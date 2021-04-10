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
        private QuestionVM QuestionVM = new QuestionVM() {Answers=new List<Quiz.Shared.AnswerDto>() };
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
            int count = QuestionVM.Answers.Where(item => item.IsCorrect == true).Count();
            if (count==1)
            {
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
            if (categories != null && categories.Count() > 0) QuestionVM.CategoryId = categories[0].Id;
            await base.OnInitializedAsync();
        }

        private void AddAnswer()
        {
           QuestionVM.Answers.Add(new Quiz.Shared.AnswerDto() { Content=null});
        }

        //void UnCheckOthers(Quiz.Shared.Answer answer, object checkedValue)
        //{
        //    var tempanswer = QuestionVM.Answers.SingleOrDefault(item => item == answer);
        //    if (tempanswer != null && (bool)checkedValue)
        //    {
        //        QuestionVM.Answers.ForEach(item => item.IsCorrect = false);
        //        tempanswer.IsCorrect = true;
        //    }
           
        //}






    }
}
