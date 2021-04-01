using Blazored.LocalStorage;
using Quiz.Shared;
using Quiz.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Quiz.Client.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;

        public CategoryService(HttpClient client, ILocalStorageService localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }




        public async Task<ResponseDto> Add(Category category)
        {
            var content = JsonSerializer.Serialize(category);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/category/add", bodyContent);
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
            var response = await _client.DeleteAsync("api/category/delete/" + id);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ResponseDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return result;
            }
            return new ResponseDto { IsSuccessful = true };

        }

        public async Task<ResponseDto> Edit(Category category)
        {
            var content = JsonSerializer.Serialize(category);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("api/category/edit", bodyContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ResponseDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return result;
            }
            return new ResponseDto { IsSuccessful = true };
        }

        public async Task<Category> Get(Guid id)
        {
            var response = await _client.GetAsync("api/category/" + id);
            var responseContent = await response.Content.ReadAsStringAsync();
         
            var result = JsonSerializer.Deserialize<Category>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }

        public async Task<List<Category>> Get()
        {
            var response = await _client.GetAsync("api/category");
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<Category>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }
    }
}
