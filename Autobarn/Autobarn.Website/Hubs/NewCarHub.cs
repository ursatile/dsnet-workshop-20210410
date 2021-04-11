using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Autobarn.Website.Hubs {
    public class NewCarHub : Hub {
        public async Task SendMessage(string user, string message) {
            await Clients.All.SendAsync("DoMessage", user, message);
        }
    }
}