using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        public string GetConnectionId() =>
        Context.ConnectionId;
    }
}