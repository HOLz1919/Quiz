using Microsoft.AspNetCore.SignalR;
using Quiz.Server.Services;
using Quiz.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Server.Hubs
{
    public class SingleTableHub : Hub
    {
        private readonly IGameService _gameService;

        public SingleTableHub(IGameService gameService)
        {
            _gameService = gameService;
        }

        public Task JoinGroup(string group)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public async Task StartMatch(string group)
        {
            await Clients.Group(group).SendAsync("StartMatch", "Mecz za chwilę się rozpocznie");
        }


     
    }
}
