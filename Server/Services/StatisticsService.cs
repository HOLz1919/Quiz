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
    public class StatisticsService : IStatisticsService
    {

        private readonly ApplicationDbContext _db;

        public StatisticsService(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<List<StatisticsView>> GetAsync(string userId)
        {
            try
            {
                var results = await _db.StatisticsViews.Where(item=> item.ApplicationUserId==userId).ToListAsync();
                return results;
            }catch(Exception ex)
            {
                Console.Write(ex);
                return null;
            }
    
        }

      
    }
}
