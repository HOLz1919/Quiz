using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quiz.Server.Data;
using Quiz.Server.Models;
using Quiz.Shared;
using Quiz.Shared.Dictionaries;
using Quiz.Shared.Responses;
using Quiz.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Server.Services
{
    public class GameService : IGameService
    {

        private readonly ApplicationDbContext _db;

        public GameService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<MatchResponseDto> AddAsync(Match match)
        {
            MatchResponseDto responseDto = new MatchResponseDto();
            try
            {
                match.Status = (int)MatchStatus.WAITING;
                await _db.Matches.AddAsync(match);
                await _db.SaveChangesAsync();
                UserMatch userMatch = new UserMatch()
                {
                    ApplicationUserId = match.OwnerId.ToString(),
                    MatchId = match.Id,
                    Points = 0

                };
                await _db.UserMatches.AddAsync(userMatch);
                await _db.SaveChangesAsync();
                await GenerateQuestionsForMatch(match.Id, match.CategoryId);

                responseDto.IsSuccessful = true;
                responseDto.MatchId = match.Id;
            }
            catch (Exception ex)
            {
                responseDto.IsSuccessful = false;
                responseDto.ErrorMessage = ex.Message;
            }
            return responseDto;
        }

        public async Task<List<MatchView>> GetAsync()
        {
            try
            {
                List<MatchView> matches = new List<MatchView>();
                List<Task> tasks = new List<Task>();
                matches = await _db.MatchViews.Where(item => item.Status == (int)MatchStatus.WAITING).ToListAsync();
                foreach (var item in matches)
                {
                    tasks.Add(ProcessMatchView(item));
                }

                var result = Task.WhenAll(tasks);
                return matches;
            }catch(Exception ex)
            {
                Console.Write(ex);
                return null;
            }
    
        }

        public async Task<MatchView> GetAsync(Guid matchId)
        {
            try
            {
              var match= await _db.MatchViews.SingleAsync(item => item.Id == matchId);
                if (match != null)
                {
                    match.Players= await _db.UserMatchViews.Where(x => x.MatchId == matchId).ToListAsync();
                }

                return match;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }

        }


        public async Task<ResponseDto> JoinAsync(Guid matchId, string userId)
        {
            ResponseDto responseDto = new ResponseDto();
            try
            {
                var result = await _db.Matches.SingleAsync(item => item.Id == matchId);
                if(_db.UserMatches.Where(item => item.MatchId == matchId).Count() < result.MaxCountOfPlayers)
                {
                    UserMatch userMatch = new UserMatch();
                    userMatch.Points = 0;
                    userMatch.MatchId = matchId;
                    userMatch.ApplicationUserId = userId;
                    await _db.UserMatches.AddAsync(userMatch);
                    await _db.SaveChangesAsync();
                    responseDto.IsSuccessful = true;
                }
                else
                {
                    responseDto.IsSuccessful = false;
                    responseDto.ErrorMessage = "You cannot join the game because the player limit has been reached";
                }
              
            }catch(Exception ex)
            {
                responseDto.IsSuccessful = false;
                responseDto.ErrorMessage = ex.Message;
            }
            return responseDto;
        }

        public async Task<List<MatchQuestionsView>> GetQuestions(Guid matchId)
        {
            try
            {
                _db.ChangeTracker.LazyLoadingEnabled = false;
                var matchQuestions = await _db.MatchQuestionsView.Where(item => item.MatchId == matchId).ToListAsync();
                List<Task> tasks = new List<Task>();
                foreach (var item in matchQuestions)
                {
                    tasks.Add(ProcessQuestionAnswer(item));
                }

                var result = Task.WhenAll(tasks);
                _db.ChangeTracker.LazyLoadingEnabled = true;
                return matchQuestions;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
        }

        public async Task<List<UserMatchView>> GetResults(Guid MatchId)
        {
            try
            {
                var scores = await _db.UserMatchViews.Where(item => item.MatchId == MatchId).OrderByDescending(item => item.Points).ToListAsync();
                return scores;

            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }


        }


        public async Task SetMatchStatus(Guid matchId, int status = 2)
        {
            var match = await _db.Matches.FirstOrDefaultAsync(item => item.Id == matchId);
            if (match != null)
            {
                match.Status = status;
                await _db.SaveChangesAsync();
            }
        }

        private Task ProcessMatchView(MatchView matchView)
        {
            matchView.Players=  _db.UserMatchViews.Where(x => x.MatchId == matchView.Id).ToList();

            return Task.CompletedTask;
            
        }

        private async Task GenerateQuestionsForMatch(Guid MatchId, Guid CategoryId)
        {
            List<MatchQuestion> Questions = await _db.Questions.Where(item => item.CategoryId == CategoryId).Select(item => new MatchQuestion() { MatchId = MatchId, QuestionId = item.Id })
                .OrderBy(r => Guid.NewGuid()).Take(3).ToListAsync();

            await _db.MatchQuestions.AddRangeAsync(Questions);
            await _db.SaveChangesAsync();
        }

 


        private Task ProcessQuestionAnswer(MatchQuestionsView matchQuestionsView)
        {
            matchQuestionsView.Answers = _db.Answers.Where(x => x.QuestionId == matchQuestionsView.QuestionId).Select(item=> new AnswerVM() { AnswerId=item.Id, Content=item.Content, IsCorrect=item.IsCorrect }).OrderBy(item => Guid.NewGuid()).ToList();

            return Task.CompletedTask;

        }

        public async Task<ResponseDto> UpdatePoints(UserMatchView userMatchView)
        {
            ResponseDto responseDto = new ResponseDto();
            try
            {
                var item = await _db.UserMatches.FirstOrDefaultAsync(item => item.MatchId == userMatchView.MatchId && item.ApplicationUserId == userMatchView.ApplicationUserId);

                if(item != null)
                {
                    item.Points += userMatchView.Points;
                    
                }
                await _db.SaveChangesAsync();
                responseDto.IsSuccessful = true;
                return responseDto;
            }
            catch(Exception ex)
            {
                responseDto.IsSuccessful = false;
                responseDto.ErrorMessage = ex.Message;
            }
            throw new NotImplementedException();
        }
    }
}
