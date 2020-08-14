using System;
using System.Threading.Tasks;
using ChatApp.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Security.Claims;
using ChatApp.Database;
using ChatApp.Models;
namespace ChatApp.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ChatController : Controller
    {
        private IHubContext<ChatHub> _chat;

        public ChatController(IHubContext<ChatHub> chat)
        {
            _chat = chat;
        }

        [HttpPost("[action]/{connectionId}/{roomId}")]
        public async Task<IActionResult> JoinRoom(string connectionId, string roomId)
        {
            await _chat.Groups.AddToGroupAsync(connectionId, roomId);
            return Ok();
        }
        [HttpPost("[action]/{connectionId}/{roomId}")]
        public async Task<IActionResult> LeaveRoom(string connectionId, string roomId)
        {
            await _chat.Groups.RemoveFromGroupAsync(connectionId, roomId);
            return Ok();
        }
        [HttpPost("/Chat/SendMessage")]
        public async Task<IActionResult> SendMesage(
            int roomId,
            string message, 
            [FromServices]AppDbContext ctx)
        {
            var Message = new Message
            {
                ChatID = roomId,
                Text = message,
                Name = User.Identity.Name,
                Timestamp = DateTime.Now
            };
            ctx.Messages.Add(Message);
            await ctx.SaveChangesAsync();
            await _chat.Clients.Group(roomId.ToString())
                .SendAsync("ReceiveMessage", new{
                    Text = Message.Text,
                    Name = Message.Name,
                    Timestamp = Message.Timestamp.ToString("dd/MM/yyyy hh:mm:ss") 
                });
            return Ok();
        }
    }
}