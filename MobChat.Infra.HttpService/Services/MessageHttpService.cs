using Microsoft.AspNetCore.SignalR.Client;
using MobChat.Infra.HttpService.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Infra.HttpService.Services
{
    public class MessageHttpService : IMessageHttpService
    {
        private HubConnection hubConnection;

        public MessageHttpService()
        {
            string url = "https://:5000/Hubs/ChatHub";
            hubConnection = new HubConnectionBuilder()
               .WithUrl(url)
               .Build();

            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                Console.WriteLine($"Mensagem - {message}");
            });
            hubConnection.On<string, string>("Conectado", (user, message) =>
            {
                Console.WriteLine("Conectado!!!!");
            });

        }

        public async Task ConnectToHub()
        {
            try
            {
                await hubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na conexão {ex.Message}");
            }
            
        }

        public async Task ConnectUser(Guid userId)
        {
            await hubConnection.SendAsync("ConnectUser", userId);
        }

        public async Task DisconnectUser(Guid userId)
        {
            await hubConnection.InvokeAsync("Disconnect", userId);
        }

        public async Task<bool> SendTextMessage(Guid userId, Guid contactId, string serializedMessage)
        {
            try
            {
                await hubConnection.SendAsync("SendMessage", userId, contactId, serializedMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
