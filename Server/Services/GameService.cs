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

        private Task ProcessMatchView(MatchView matchView)
        {
            matchView.Players=  _db.UserMatchViews.Where(x => x.MatchId == matchView.Id).ToList();

            return Task.CompletedTask;
            
        }

    }
}
