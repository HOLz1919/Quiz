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
    public class GameController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IGameService _gameService;
        private readonly IHubContext<TablesHub> _hubContext;
        private readonly IHubContext<SingleTableHub> _hubContextSingleTable;

        public GameController(ApplicationDbContext db, IGameService matchService, IHubContext<TablesHub> hubContext, IHubContext<SingleTableHub> hubContextSingleTable)
        {
            _db = db;
            _gameService = matchService;
            _hubContext = hubContext;
            _hubContextSingleTable = hubContextSingleTable;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _gameService.GetAsync();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var match = await _gameService.GetAsync(id);
            if (match == null)
                return NotFound(new { ErrorMessage = "Not found match with that id" });
            return Ok(match);
        }

        [HttpPost("Join")]
        public async Task<IActionResult> Join([FromBody] UserMatchDto userMatch)
        {
            var result = await _gameService.JoinAsync(userMatch.matchId, userMatch.userId);
            if (!result.IsSuccessful)
                return StatusCode(400, result);

            var matches = await _gameService.GetAsync();
            await _hubContext.Clients.All.SendAsync("MatchesUpdate", matches);

            var match = await _gameService.GetAsync(userMatch.matchId);
            await _hubContextSingleTable.Clients.Group(userMatch.matchId.ToString()).SendAsync("UpdateMatch", match);
    

            return Ok(result);

        }



        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] Match match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ErrorMessage = "Model is not Valid" });
            }

            var result = await _gameService.AddAsync(match);

            if (!result.IsSuccessful)
                return BadRequest(result);


            var matches = await _gameService.GetAsync();
            await _hubContext.Clients.All.SendAsync("MatchesUpdate", matches);

            return Ok(result);
        }


        [HttpGet("GetQuestions/{id}")]
        public async Task<IActionResult> GetQuestions(Guid id)
        {
            var questions = await _gameService.GetQuestions(id);
            return Ok(questions);

        }

        [HttpGet("GetResults/{id}")]
        public async Task<IActionResult> GetResults(Guid id)
        {
            var results = await _gameService.GetResults(id);
            return Ok(results);

        }

        [HttpPut("UpdatePoints")]
        public async Task<IActionResult> UpdatePoints([FromBody] UserMatchView userMatchView)
        {
            var result = await _gameService.UpdatePoints(userMatchView);

            if (!result.IsSuccessful)
                return BadRequest(result);

            var scores = await _gameService.GetResults(userMatchView.MatchId);
            await _hubContextSingleTable.Clients.Group(userMatchView.MatchId.ToString()).SendAsync("UpdateResult", scores);

            return Ok(result);
        }

        [HttpPut("EndMatch")]
        public async Task<IActionResult> EndMatch([FromBody] Guid MatchId)
        {
            var result = await _gameService.EndMatch(MatchId);

            if (!result.IsSuccessful)
                return BadRequest(result);


            return Ok(result);
        }









    }
}
