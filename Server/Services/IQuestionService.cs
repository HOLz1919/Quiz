using Quiz.Shared;
using Quiz.Shared.Responses;
using Quiz.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Server.Services
{
    public interface IQuestionService
    {
        Task<ResponseDto> AddAsync(QuestionVM questionVM);
        Task<QuestionVM> GetAsync(Guid id);
        Task<ResponseDto> EditAsync(QuestionVM questionVM);
        Task<ResponseDto> DeleteAsync(Guid id);
        Task<List<QuestionView>> GetAsync();


    }
}
