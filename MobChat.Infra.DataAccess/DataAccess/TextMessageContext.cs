using Microsoft.EntityFrameworkCore;
using MobChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobChat.Infra.DataAccess.DataAccess
{
    public class TextMessageContext : DbContext
    {
        private readonly string dbConnectionString;
        public DbSet<TextMessage> TextMessages { get; set; }

        public TextMessageContext()
        {
            this.dbConnectionString = "";
            Database.EnsureCreated();
        }

        public TextMessageContext(string dbConnectionString)
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
