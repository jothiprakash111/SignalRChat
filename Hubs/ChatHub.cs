using Microsoft.AspNetCore.SignalR;
using NuGet.Protocol.Plugins;
using System.Linq;
using WebApi.Helpers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        DataContext dataContext;
        public ChatHub(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public async Task SendMessageToAll(int sender, string message)
        {
            await Clients.All.SendAsync("ReceiveAllMessage", sender, message);
        }

        public async Task SendMessageToUser(string senderId, string receiverId, string message)
        {

            var senderConnectionId = this.dataContext.Users.Where(x=>x.Id.Equals(senderId)).FirstOrDefault().ConnectionId;
            var reciverConnectionId = this.dataContext.Users.Where(x => x.Id.Equals(receiverId)).FirstOrDefault().ConnectionId;


            await Clients.Clients(new string[] {senderConnectionId,reciverConnectionId }).SendAsync("ReceiveMessage", senderId, receiverId, message);
        }

        public async Task TwoUserGroup(string senderId, string receiverId, string message)
        {


            

            var senderConnectionId = this.dataContext.Users.Where(x => x.Id.Equals(senderId)).FirstOrDefault().ConnectionId;
            var reciverConnectionId = this.dataContext.Users.Where(x => x.Id.Equals(receiverId)).FirstOrDefault().ConnectionId;


            await Clients.Clients(new string[] { senderConnectionId, reciverConnectionId }).SendAsync("ReceiveMessage", senderId, receiverId, message);
        }

        public async Task SendMessageToGroup(int senderId, int groupId, string message)
        {
            await Clients.Group(groupId.ToString()).SendAsync("GroupMessage", senderId, message);
        }

        public  void SetConnection(string userId,string connectionId)
        {
            var user = this.dataContext.Users.Where(x => x.Id == userId).FirstOrDefault();

            if(user != null)
            {
                user.ConnectionId = connectionId;
                user.IsOnline = true;

                this.dataContext.Update(user);
                this.dataContext.SaveChanges();
            }

        }


    }
}
