using Quiz.Server.Models;
using Quiz.Shared;
using Quiz.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Server.Services
{
    public interface ICategoryService
    {
        Task<ResponseDto> AddAsync(Category category);
        Task<Category> GetAsync(Guid id);
        Task<ResponseDto> EditAsync(Category category);
        Task<ResponseDto> DeleteAsync(Guid id);
        Task<List<Category>> GetAsync();
    }
}
