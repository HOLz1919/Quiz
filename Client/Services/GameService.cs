using Quiz.Shared;
using Quiz.Shared.Models;
using Quiz.Shared.Responses;
using Quiz.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Quiz.Client.Services
{
    public class GameService : IGameService
    {

        private readonly HttpClient _client;

        public GameService(HttpClient client)
        {
            _client = client;
        }

        public async Task<MatchResponseDto> Add(MatchDto match)
        {
            var content = JsonSerializer.Serialize(match);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/game/add", bodyContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<MatchResponseDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }

        public async Task<ResponseDto> EndMatch(Guid matchId)
        {
            var content = JsonSerializer.Serialize(matchId);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("api/game/EndMatch", bodyContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ResponseDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return result;
            }
            return new ResponseDto { IsSuccessful = true };
        }

        public async Task<List<MatchView>> Get()
        {
            var response = await _client.GetAsync("api/game");
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<MatchView>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }

        public async Task<MatchView> Get(Guid matchId)
        {
            var response = await _client.GetAsync("api/game/"+matchId);
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<MatchView>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }

        public async Task<List<MatchQuestionsView>> GetQuestions(Guid matchId)
        {
            var response = await _client.GetAsync("api/game/getquestions/" + matchId);
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<MatchQuestionsView>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }

        public async Task<List<UserMatchView>> GetResults(Guid matchId)
        {
            var response = await _client.GetAsync("api/game/getresults/" + matchId);
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<UserMatchView>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }

        public async Task<UserMoneyDto> GetUserMoney(string userId)
        {
            var response = await _client.GetAsync("api/user/GetUserMoney/" + userId);
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<UserMoneyDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }

        public async Task<ResponseDto> Join(Guid matchId, string userId)
        {
            UserMatchDto userMatch = new UserMatchDto() { matchId = matchId, userId = userId };
            var content = JsonSerializer.Serialize(userMatch);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/game/join", bodyContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ResponseDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }

        public async Task<ResponseDto> UpdateUserScore(string userId, Guid matchId, int pointsForQuestions)
        {
            UserMatchView userMatchView = new UserMatchView() { MatchId = matchId, ApplicationUserId = userId, Points = pointsForQuestions };
            var content = JsonSerializer.Serialize(userMatchView);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("api/game/updatePoints", bodyContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ResponseDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return result;
            }
            return new ResponseDto { IsSuccessful = true };
        }
    }
}
