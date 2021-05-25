using Quiz.Shared;
using Quiz.Shared.Responses;
using Quiz.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Client.Services
{
    public interface IGameService
    {
        Task<MatchResponseDto> Add(MatchDto match);
        Task<List<MatchView>> Get();
        Task<MatchView> Get(Guid matchId);
        Task<ResponseDto> Join(Guid matchId, string userId);
        Task<List<MatchQuestionsView>> GetQuestions(Guid matchId);
        Task<List<UserMatchView>> GetResults(Guid matchId);
        Task<ResponseDto> UpdateUserScore(string userId, Guid matchId, int pointsForQuestions);
    }
}
