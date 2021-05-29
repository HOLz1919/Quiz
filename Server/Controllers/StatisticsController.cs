using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Quiz.Server.Data;
using Quiz.Server.Hubs;
using Quiz.Server.Models;
using Quiz.Server.Services;
using Quiz.Shared.Models;
using Quiz.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatisticsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(ApplicationDbContext db, IStatisticsService statisticsService)
        {
            _db = db;
            _statisticsService = statisticsService;
       
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _statisticsService.GetAsync(id);
            return Ok(result);
        }



    }
}
