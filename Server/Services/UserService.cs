using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quiz.Server.Data;
using Quiz.Server.Models;
using Quiz.Shared;
using Quiz.Shared.Dictionaries;
using Quiz.Shared.Responses;
using Quiz.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Server.Services
{
    public class UserService : IUserService
    {

        private readonly ApplicationDbContext _db;

        public UserService(ApplicationDbContext db)
        {
            _db = db;
        }



        public async Task<List<UserVM>> GetAsync()
        {
            try
            {
                List <UserVM> users = new List<UserVM>();
                users = await _db.Users.Select(item =>new UserVM() {Id=item.Id, FirstName=item.FirstName, LastName=item.LastName, Email=item.Email, Money=item.Money, Username=item.UserName }).ToListAsync();

                return users;
            }catch(Exception ex)
            {
                Console.Write(ex);
                return null;
            }
    
        }

    }
}
