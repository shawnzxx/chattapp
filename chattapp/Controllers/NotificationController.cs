using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chattapp.Hub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace chattapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : Controller
    {
        private readonly IHubContext<GitHubChatSampleHub> _hubContext;
        public NotificationController(IHubContext<GitHubChatSampleHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public IActionResult NotifyUserWithMessage([FromBody]NotificationDTO notification)
        {
            string instanceName = System.Environment.MachineName;
            try
            {
                string userName = notification.UserName;
                string notifyMessage = $"{notification.Message} from {instanceName}";
                _hubContext.Clients.Group(userName).SendAsync("notify", notifyMessage);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok("[OK]");
        }
    }
}