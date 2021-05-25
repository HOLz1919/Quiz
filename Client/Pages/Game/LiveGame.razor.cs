using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Quiz.Client.Services;
using Quiz.Shared.ViewModels;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Client.Pages.Game
{
    public partial class LiveGame
    {
        [Parameter]
        public Guid MatchId { get; set; }
        public List<MatchQuestionsView> MatchQuestions = new List<MatchQuestionsView>();
        public MatchQuestionsView tempQuestion = new MatchQuestionsView() { Answers = new List<AnswerVM>() };
        public List<UserMatchView> Scores = new List<UserMatchView>();
        private string UserId { get; set; }
        [Inject]
        public IGameService GameService { get; set; }
        [Inject]
        public TooltipService tooltipService { get; set; }
        [Inject]
        public DialogService DialogService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public ILocalStorageService _localStorage { get; set; }

        public HubConnection Connection { get; set; }
        private int PointsForQuestions { get; set; }
        private int Counter { get; set; } = 1000;
        private int BreakCounter { get; set; } = 2;
        private static System.Timers.Timer aTimer { get; set; }
        private static System.Timers.Timer BreakTimer { get; set; }
        private int CountOfQuestions { get; set; }
        private int ActualIteration { get; set; } = 0;
        private bool IsBreak { get; set; } = false;


        protected override async Task OnInitializedAsync()
        {

            UserId = await _localStorage.GetItemAsync<string>("UserId");
            MatchQuestions = await GameService.GetQuestions(MatchId);
            CountOfQuestions = MatchQuestions.Count();
            Scores = await GameService.GetResults(MatchId);
            StartBreakTimer();
            await ConnectToServer();
            await  base.OnInitializedAsync();
        }



        private async Task ConnectToServer()
        {
            Connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44352/SingleTableHub")
                .Build();

            await Connection.StartAsync();

            Connection.Closed += async (s) =>
            {
                await Connection.StartAsync();
            };

            await Connection.InvokeAsync("JoinGroup", MatchId);

            Connection.On<List<UserMatchView>>("UpdateResult", s =>
            {
                Scores = s;
                StateHasChanged();
            });


        }

        


        public void StartTimer()
        {
            Counter = 1000;
            IsBreak = false;
            aTimer = new System.Timers.Timer(100);
            aTimer.Elapsed += CountDownTimer;
            tempQuestion = MatchQuestions[ActualIteration];
            aTimer.Enabled = true;
        }

        public void CountDownTimer(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (Counter > 0)
            {
                Counter -= 10;
            }
            else
            {
                aTimer.Enabled = false;
                ActualIteration++;
                StartBreakTimer();
            }
            InvokeAsync(StateHasChanged);
        }

        public void StartBreakTimer()
        {

            BreakCounter = ActualIteration==0?0:2;
            IsBreak = true;
            BreakTimer = new System.Timers.Timer(1000);
            BreakTimer.Elapsed += CountDownBreakTimer;
            if(ActualIteration>=CountOfQuestions) NavigationManager.NavigateTo("/game/gameresults/" + MatchId);
            BreakTimer.Enabled = true;
        }

        public void CountDownBreakTimer(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (BreakCounter > 0)
            {
                BreakCounter -= 1;
            }
            else
            {
                BreakTimer.Enabled = false;
                StartTimer();
            }
            InvokeAsync(StateHasChanged);
        }

        public async Task SelectAnswer(bool isCorrect)
        {
            if (isCorrect)
            {
                PointsForQuestions = 1000 + Counter;
                await GameService.UpdateUserScore(UserId, MatchId, PointsForQuestions);
            }

            IsBreak = true;
        }




    }
}
