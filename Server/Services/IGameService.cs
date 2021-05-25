using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quiz.Server.Models;
using Quiz.Shared;
using Quiz.Shared.Responses;
using Quiz.Shared.ViewModels;

namespace Quiz.Server.Services
{
    public interface IGameService
    {
        Task<List<MatchView>> GetAsync();
        Task<MatchView> GetAsync(Guid matchId);
        Task<MatchResponseDto> AddAsync(Match match);
        Task<ResponseDto> JoinAsync(Guid matchId, string userId);
        Task<List<MatchQuestionsView>> GetQuestions(Guid matchId);
        Task<List<UserMatchView>> GetResults(Guid MatchId);
        Task SetMatchStatus(Guid matchId, int status = 2);
        Task<ResponseDto> UpdatePoints(UserMatchView userMatchView);
    }
}
