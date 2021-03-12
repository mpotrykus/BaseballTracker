using DTOs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace MK.BaseballTracker.SignalR
{
    public class SignalRHub : Hub
    {
        public Task Send(string user, string message) 
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
