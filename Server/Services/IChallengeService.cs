﻿using Quiz.Server.Models;
using Quiz.Shared;
using Quiz.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Server.Services
{
    public interface IChallengeService
    {
        Task<ResponseDto> AddAsync(Challenge challenge);
        Task<Challenge> GetAsync(Guid id);
        Task<ResponseDto> EditAsync(Challenge challenge);
        Task<ResponseDto> DeleteAsync(Guid id);
        Task<List<Challenge>> GetAsync();
    }
}
