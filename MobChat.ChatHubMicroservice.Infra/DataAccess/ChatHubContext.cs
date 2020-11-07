using Microsoft.EntityFrameworkCore;
using MobChat.ChatHubMicroservice.Domain.AggregatesModel.ConnectedUserAggregate;
using MobChat.ChatHubMicroservice.Domain.AggregatesModel.OfflineTextMessageAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobChat.ChatHubMicroservice.Infra.DataAccess
{
    public class ChatHubContext : DbContext
    {
        private readonly string dbConnectionString;
        public DbSet<ConnectedUser> ConnectedUser { get; set; }
        public DbSet<OfflineTextMessage> OfflineTextMessage { get; set; }

        public ChatHubContext()
        {
            this.dbConnectionString = "Server=tcp:mobchat-chathub-db-server.database.windows.net,1433;Initial Catalog=mobchat-chathubmicroservice-db;Persist Security Info=False;User ID=felipefcrj;Password=01Xam-Project-BR-77;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            Database.EnsureCreated();
        }

        public ChatHubContext(string dbConnectionString)
        {
            this.dbConnectionString = dbConnectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(dbConnectionString);
        }
    }
}
