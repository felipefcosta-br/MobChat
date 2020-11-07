using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using MobChat.ChatHubMicroservice.Domain.AggregatesModel.ConnectedUserAggregate;
using MobChat.ChatHubMicroservice.Domain.AggregatesModel.OfflineTextMessageAggregate;
using MobChat.ChatHubMicroservice.Infra.Repositories.ConnectedUsers;
using MobChat.ChatHubMicroservice.Infra.Repositories.OfflineTextMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobChat.ChatHubMicroservice.Api.Hubs
{
    //[Authorize]
    public class ChatHub : Hub
    {
        private readonly IConnectedUserService userService = new ConnectedUserService(new AzureSqlServerConnectedUserRepository());
        private readonly IOfflineTextMessageService messageService = new OfflineTextMessageService(new AzureSqlServerOfflineTextMessageRepository());

        public async Task ConnectUser(Guid userId)
        {
            String connectionUserId;
            bool isConnected = await userService.IsConnected(userId);
            if (isConnected)
            {
                ConnectedUser connectedUser = await userService.GetConnectedUserByUserIdAsync(userId);
                connectionUserId = connectedUser.ContextUserId;
            }
            else
            {
                connectionUserId = Context.UserIdentifier;
                await userService.AddConnectedUserAsync(userId, connectionUserId, Context.ConnectionId);
            }
            

            await Clients.User(connectionUserId).SendAsync("Conectado!", userId);
            Console.WriteLine($"Conectado! {connectionUserId}");
        }

        public async Task DisconnectUser(Guid userId)
        {
            ConnectedUser connectedUser = await userService.GetConnectedUserByUserIdAsync(userId);            
            await userService.DeleteConnectedUserAsync(userId);
            await Clients.User(connectedUser.ConnectionId).SendAsync("Desconectado!", userId);
            Console.WriteLine($"Desconectado! {userId}");
        }

        public async void SendTextMessage(Guid senderId, Guid receiverId, string serializedMessage)
        {
            ConnectedUser connectedReceiver = await userService.GetConnectedUserByUserIdAsync(receiverId);

            if (connectedReceiver != null)
            {
                await Clients.User(connectedReceiver.ContextUserId).SendAsync("ReceiveMessage", connectedReceiver.UserId, serializedMessage);
            }
            else
            {
                await messageService.AddOfflineMessageAsync(receiverId, senderId, serializedMessage);
            }
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }
}
