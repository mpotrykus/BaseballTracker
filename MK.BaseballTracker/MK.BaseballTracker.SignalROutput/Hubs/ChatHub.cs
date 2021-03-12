using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace MK.BaseballTracker.SignalROutput.Hubs
{
    public class ChatHub : Hub
    {
        // Send message
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
