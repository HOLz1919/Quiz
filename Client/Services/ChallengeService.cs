using Blazored.LocalStorage;
using Quiz.Shared;
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
    public class ChallengeService : IChallengeService
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;

        public ChallengeService(HttpClient client, ILocalStorageService localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }




        public async Task<ResponseDto> Add(ChallengeDto challenge)
        {
            var content = JsonSerializer.Serialize(challenge);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/challenge/add", bodyContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ResponseDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return result;
            }
            return new ResponseDto { IsSuccessful = true };
        }

        public async Task<ResponseDto> Delete(Guid id)
        {
            var response = await _client.DeleteAsync("api/challenge/delete/" + id);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ResponseDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return result;
            }
            return new ResponseDto { IsSuccessful = true };

        }

        public async Task<ResponseDto> Edit(ChallengeDto challenge)
        {
            var content = JsonSerializer.Serialize(challenge);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("api/challenge/edit", bodyContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ResponseDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return result;
            }
            return new ResponseDto { IsSuccessful = true };
        }

        public async Task<ChallengeDto> Get(Guid id)
        {
            var response = await _client.GetAsync("api/challenge/" + id);
            var responseContent = await response.Content.ReadAsStringAsync();
         
            var result = JsonSerializer.Deserialize<ChallengeDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }

        public async Task<List<ChallengeView>> Get()
        {
            var response = await _client.GetAsync("api/challenge");
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<ChallengeView>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }
    }
}
