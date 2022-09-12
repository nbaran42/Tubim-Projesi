using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.SignalR;

namespace TubimProject.Infrastructure.SignalR
{
    public class UpdateDashboardHubClient : Hub<IUpdateDashboardHubClient>
    {
        public async Task UpdateDashboard()
        {
            await Clients.All.UpdateDashboard();
        }
    }
}
