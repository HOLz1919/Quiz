using Microsoft.AspNetCore.Components;
using Quiz.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Radzen;
using Quiz.Shared;
using Radzen.Blazor;
using Quiz.Shared.ViewModels;

namespace Quiz.Client.Pages.Administration.Question
{
    public partial class Questions
    {
        [Inject]
        public ICategoryService CategoryService { get; set; }
        [Inject]
        public IQuestionService QuestionService { get; set; }
        [Inject]
        public TooltipService tooltipService { get; set; }
        [Inject]
        public DialogService DialogService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }


        int count;
        IEnumerable<QuestionView> questions;
        RadzenGrid<QuestionView> questionsGrid;


        async Task LoadData(LoadDataArgs args)
        {
            questions = await QuestionService.Get();

            if (!string.IsNullOrEmpty(args.Filter))
            {
                questions = questions.AsQueryable().Where(args.Filter).ToList();
            }

            if (!string.IsNullOrEmpty(args.OrderBy))
            {
                questions = questions.AsQueryable().OrderBy(args.OrderBy).ToList();
            }

            questions = questions.Skip(args.Skip.Value).Take(args.Top.Value).ToList();

            count = questions.Count();

            await InvokeAsync(StateHasChanged);
        }

        async Task Edit(QuestionView question)
        {
            await Task.Run(() => NavigationManager.NavigateTo("/administration/questions/edit/" + question.Id));
        }

        void Add()
        {
            NavigationManager.NavigateTo("/administration/questions/add");
        }



       

    }
}
