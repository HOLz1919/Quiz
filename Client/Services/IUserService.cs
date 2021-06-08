using Quiz.Shared;
using Quiz.Shared.Responses;
using Quiz.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Client.Services
{
    public interface IUserService
    {
        Task<List<UserVM>> Get();
        Task<UserVM> Get(string UserId);
        Task<ResponseDto> Edit(UserVM user);
        Task<ResponseDto> Delete(string id);
        Task<ResponseDto> Add(AddUserVM user);
    }
}
