using Microsoft.EntityFrameworkCore;
using MobChat.ChatMicroservice.Domain.AggregatesModel.ConnectedUserAggregate;
using MobChat.ChatMicroservice.Domain.AggregatesModel.OfflineTextMessageAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobChat.ChatMicroservice.Infra.DataAccess
{
    public class ChatContext : DbContext
    {
        private readonly string dbConnectionString;
        public DbSet<ConnectedUser> ConnectedUser { get; set; }
        public DbSet<OfflineTextMessage> OfflineTextMessage { get; set; }

        public ChatContext()
        {
            this.dbConnectionString = "Server=tcp:mobchat-chatmicroservice-db-server.database.windows.net,1433;Initial Catalog=mobchat-chatmicroservice-db;Persist Security Info=False;User ID=felipefcrj;Password=01Xam-Project-BR-77;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            Database.EnsureCreated();
        }

        public ChatContext(string dbConnectionString)
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
