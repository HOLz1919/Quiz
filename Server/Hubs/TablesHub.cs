using Microsoft.AspNetCore.SignalR;
using Quiz.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Server.Hubs
{
    public class TablesHub : Hub
    {
        //public async Task MatchesUpdate(List<MatchView> matches)
        //{
        //    await Clients.All.SendAsync("MatchesUpdate", matches);
        //}
    }
}
