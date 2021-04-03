using Quiz.Shared;
using Quiz.Shared.Responses;
using Quiz.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Client.Services
{
    public interface IQuestionService
    {

        Task<ResponseDto> Add(QuestionVM questionVM);
        Task<QuestionVM> Get(Guid id);
        Task<ResponseDto> Edit(QuestionVM questionVM);
        Task<ResponseDto> Delete(Guid id);
        Task<List<QuestionView>> Get();
    }
}
