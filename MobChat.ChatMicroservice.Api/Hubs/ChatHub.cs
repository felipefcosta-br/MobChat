using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using MobChat.ChatMicroservice.Domain.AggregatesModel.ConnectedUserAggregate;
using MobChat.ChatMicroservice.Domain.AggregatesModel.OfflineTextMessageAggregate;
using MobChat.ChatMicroservice.Infra.Repositories.ConnectedUsers;
using MobChat.ChatMicroservice.Infra.Repositories.OfflineTextMessages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobChat.ChatMicroservice.Api.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
                connectedUser.ConnectionId = Context.ConnectionId;
                connectedUser.ContextUserId = Context.User.Identity.Name;
                userService.UpdateConnectedUser(connectedUser);
               
            }
            else
            {
                connectionUserId = Context.User.Identity.Name;
                await userService.AddConnectedUserAsync(userId, connectionUserId, Context.ConnectionId);
            }

            IEnumerable<OfflineTextMessage> offlineMessages = messageService.GetOfflineTextMessagesAsync(userId);
            string serializedMessages = JsonConvert.SerializeObject(offlineMessages);
            await Clients.Caller.SendAsync("Conectado", userId, serializedMessages);
        }

        public async Task DisconnectUser(Guid userId)
        {
            ConnectedUser connectedUser = await userService.GetConnectedUserByUserIdAsync(userId);

            await userService.DeleteConnectedUserAsync(userId);
            Console.WriteLine($"Desconectado! {userId}");
        }

        public async Task SendTextMessage(Guid senderId, Guid receiverId, string serializedMessage)
        {
            ConnectedUser connectedReceiver = await userService.GetConnectedUserByUserIdAsync(receiverId);            

            if (connectedReceiver != null)
            {
                Console.WriteLine($"Mensagem! {connectedReceiver.ConnectionId}");
                await Clients.Client(connectedReceiver.ConnectionId).SendAsync("ReceiveMessage", connectedReceiver.UserId, serializedMessage);
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
