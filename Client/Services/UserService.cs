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
    public class UserService : IUserService
    {

        private readonly HttpClient _client;

        public UserService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ResponseDto> Add(AddUserVM user)
        {
            var content = JsonSerializer.Serialize(user);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/user/add", bodyContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ResponseDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return result;
            }
            return new ResponseDto { IsSuccessful = true };
        }

        public async Task<ResponseDto> Delete(string id)
        {
            var response = await _client.DeleteAsync("api/user/delete/" + id);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ResponseDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return result;
            }
            return new ResponseDto { IsSuccessful = true };
        }

        public async Task<ResponseDto> Edit(UserVM user)
        {
            var content = JsonSerializer.Serialize(user);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("api/user/edit", bodyContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ResponseDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return result;
            }
            return new ResponseDto { IsSuccessful = true };
        }

        public async Task<List<UserVM>> Get()
        {
            var response = await _client.GetAsync("api/user");
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<UserVM>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }

        public async Task<UserVM> Get(string UserId)
        {
            var response = await _client.GetAsync("api/user/" + UserId);
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<UserVM>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }
    }
}
