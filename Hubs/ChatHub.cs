using Microsoft.AspNetCore.SignalR;
using System.Linq;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessageToAll(int sender, string message)
        {
            await Clients.All.SendAsync("ReceiveAllMessage", sender, message);
        }

        public async Task SendMessageToUser(int senderId, int receiverId, string message)
        {
            await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", senderId, receiverId, message);
        }

        public async Task SendMessageToGroup(int senderId, int groupId, string message)
        {
            await Clients.Group(groupId.ToString()).SendAsync("ReceiveMessage", senderId, message);
        }
    }
}
