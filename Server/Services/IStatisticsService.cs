using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quiz.Server.Models;
using Quiz.Shared;
using Quiz.Shared.Responses;
using Quiz.Shared.ViewModels;

namespace Quiz.Server.Services
{
    public interface IStatisticsService
    {
        Task<List<StatisticsView>> GetAsync(string userId);
    }
}
