using Microsoft.EntityFrameworkCore;
using MobChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobChat.Infra.DataAccess.DataAccess
{
    public class ChatContext : DbContext
    {
        private readonly string dbConnectionString;
        public DbSet<Chat> Chats { get; set; }
        public DbSet<TextMessage> TextMessages { get; set; }

        public ChatContext()
        {
            this.dbConnectionString = "";
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
            optionsBuilder.UseSqlite(dbConnectionString);
        }
    }
}
