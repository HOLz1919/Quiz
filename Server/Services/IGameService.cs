﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quiz.Server.Models;
using Quiz.Shared;
using Quiz.Shared.Responses;

namespace Quiz.Server.Services
{
    public interface IGameService
    {
        Task<MatchDto> GetAsync();
        Task<MatchResponseDto> AddAsync(Match match);

    }
}
