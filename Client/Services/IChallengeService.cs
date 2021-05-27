using Quiz.Shared;
using Quiz.Shared.Responses;
using Quiz.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Client.Services
{
    public interface IChallengeService
    {
        Task<ResponseDto> Add(ChallengeDto category);
        Task<ChallengeDto> Get(Guid id);
        Task<ResponseDto> Edit(ChallengeDto category);
        Task<ResponseDto> Delete(Guid id);
        Task<List<ChallengeView>> Get();

    }
}
