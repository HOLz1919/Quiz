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
            List<MatchView> matches = new List<MatchView>();
            matches = await _db.MatchViews.Where(item => item.Status == (int)MatchStatus.WAITING).ToListAsync();
            foreach(var item in matches)
            {
                item.Players = await _db.UserMatchViews.Where(x => x.MatchId == item.Id).ToListAsync();
                item.CountOfPlayers = item.Players.Count();
            }
            return matches;

        }
    }
}
