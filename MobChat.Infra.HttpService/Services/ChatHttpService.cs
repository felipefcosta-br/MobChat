using Microsoft.AspNetCore.SignalR.Client;
using MobChat.Infra.HttpService.EventHandlers;
using MobChat.Infra.HttpService.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Infra.HttpService.Services
{
    public class ChatHttpService : IChatHttpService
    {
        public event EventHandler<MessageEventArgs> OnReceivedMessage;

        private HubConnection hubConnection;
        private bool isConnected;

        public ChatHttpService()
        {
            isConnected = false;
        }

        public void InitConnection(String AccessToken)
        {

            //Android
            string ip = "10.0.2.2";
            //string ip = "https://mobchatchatmicroserviceapi.azurewebsites.net";
            string url = $"http://{ip}:5000/hubs/chathub";

            hubConnection = new HubConnectionBuilder().
                WithAutomaticReconnect().
                WithUrl(url, options => { 
                    options.AccessTokenProvider = () => Task.FromResult(AccessToken);
                    options.UseDefaultCredentials = true;
                }).
                Build();

            hubConnection.On<string, string>("Conectado", (user, message) =>
            {
                Console.WriteLine(message);
            });
            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
               
                OnReceivedMessage?.Invoke(this, new MessageEventArgs(message, user));
                
            });
        }

        public async Task ConnectAsync(Guid userId)
        {
            if (isConnected)
                return;

            await hubConnection.StartAsync();
            await RegisterUser(userId);
            isConnected = true;
        }

        public async Task DisconnectAsync(Guid userId)
        {
            if (!isConnected)
                return;
            try
            {
                await RemoveUser(userId);
                await hubConnection.StopAsync();
                isConnected = false;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            
        }

        public async Task SendTextMessageAsync(Guid userId, Guid contactId, string serializedMessage)
        {

            await hubConnection.InvokeAsync("SendTextMessage", userId, contactId, serializedMessage);
        }

        public async Task RegisterUser(Guid userId)
        {
            await hubConnection.SendAsync("ConnectUser", userId);
        }
        public async Task RemoveUser(Guid userId)
        {
            await hubConnection.SendAsync("DisconnectUser", userId);
        }
    }
}
