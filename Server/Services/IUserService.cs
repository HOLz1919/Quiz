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
    public interface IUserService
    {
        Task<List<UserVM>> GetAsync();
        Task<UserVM> GetAsync(string id);
        Task<UserMoneyDto> GetMoney(string userId);
    }
}
