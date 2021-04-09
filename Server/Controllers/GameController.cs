using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quiz.Server.Data;
using Quiz.Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GameController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IGameService _matchService;

        public GameController(ApplicationDbContext db, IGameService matchService)
        {
            _db = db;
            _matchService = matchService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _matchService.GetAsync();
            return Ok(result);
        }





    }
}
