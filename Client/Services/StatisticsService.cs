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
    public class StatisticsService : IStatisticsService
    {

        private readonly HttpClient _client;

        public StatisticsService(HttpClient client)
        {
            _client = client;
        }



        public async Task<List<StatisticsView>> Get(string userId)
        {
            var response = await _client.GetAsync("api/Statistics/" + userId);
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<StatisticsView>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }

  
    }
}
