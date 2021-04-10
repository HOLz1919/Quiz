using Quiz.Shared;
using Quiz.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Client.Services
{
    public interface IGameService
    {
        Task<MatchResponseDto> Add(MatchDto match);
    }
}
