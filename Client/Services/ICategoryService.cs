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
        Task<ResponseDto> Add(CategoryDto category);
        Task<CategoryDto> Get(Guid id);
        Task<ResponseDto> Edit(CategoryDto category);
        Task<ResponseDto> Delete(Guid id);
        Task<List<CategoryDto>> Get();

    }
}
