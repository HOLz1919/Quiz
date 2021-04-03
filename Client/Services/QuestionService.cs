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
    public class QuestionService : IQuestionService
    {
        private readonly HttpClient _client;

        public QuestionService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ResponseDto> Add(QuestionVM questionVM)
        {
            var content = JsonSerializer.Serialize(questionVM);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/question/add", bodyContent);
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
            var response = await _client.DeleteAsync("api/question/delete/" + id);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ResponseDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return result;
            }
            return new ResponseDto { IsSuccessful = true };

        }

        public async Task<ResponseDto> Edit(QuestionVM questionVM)
        {
            var content = JsonSerializer.Serialize(questionVM);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("api/question/edit", bodyContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ResponseDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return result;
            }
            return new ResponseDto { IsSuccessful = true };
        }

        public async Task<QuestionVM> Get(Guid id)
        {
            var response = await _client.GetAsync("api/question/" + id);
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<QuestionVM>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }

        public async Task<List<QuestionView>> Get()
        {
            var response = await _client.GetAsync("api/question");
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<QuestionView>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }


    }
}
