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
    public class RankingController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IRankingService _rankingService;
        public RankingController(ApplicationDbContext db, IRankingService rankingService)
        {
            _db = db;
            _rankingService = rankingService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _rankingService.GetAsync();
            return Ok(result);
        }



    }
}
