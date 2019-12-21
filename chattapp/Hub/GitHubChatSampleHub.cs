using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace chattapp.Hub
{
    [Authorize]
    public class GitHubChatSampleHub : DynamicHub
    {
        public override Task OnConnectedAsync()
        {
            string instanceName = System.Environment.MachineName;

            string userName = Context.User.Identity.Name;
            Groups.AddToGroupAsync(Context.ConnectionId, userName);

            return Clients.All.BroadcastMessage("_SYSTEM_", $"{userName} JOINED Machine: {instanceName}");
        }

        // Uncomment this line to only allow user in Microsoft to send message
        //[Authorize(Policy = "Microsoft_Only")]
        public void BroadcastMessage(string message)
        {
            string userName = Context.User.Identity.Name;
            Clients.All.BroadcastMessage($"{userName}", message);
        }

        public void Echo(string message)
        {
            string userName = Context.User.Identity.Name;
            var echoMessage = $"{message} (echo from own)";
            Clients.Client(Context.ConnectionId).echo(userName, echoMessage);
        }
    }
}
