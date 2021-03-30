using Quiz.Shared;
using Quiz.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Client.Services
{
    public interface ICategoryService
    {
        Task<ResponseDto> Add(Category category);
        Task<Category> Get(Guid id);
        Task<ResponseDto> Edit(Category category);
        Task<ResponseDto> Delete(Guid id);
        Task<List<Category>> Get();

    }
}
