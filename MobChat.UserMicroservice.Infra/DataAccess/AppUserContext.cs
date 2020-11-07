using Microsoft.EntityFrameworkCore;
using MobChat.UserMicroservice.Domain.AggregatesModel.UserAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobChat.UserMicroservice.Infra.DataAccess
{
    public class AppUserContext : DbContext
    {
        private readonly string dbConnectionString;
        public DbSet<AppUser> AppUser { get; set; }

        public AppUserContext()
        {
            this.dbConnectionString = "Server=tcp:mobchat-user-db-server.database.windows.net,1433;Initial Catalog=mobchat-usermicroservice-db;Persist Security Info=False;User ID=felipefcrj;Password=00Xam-Project-BR-77;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            Database.EnsureCreated();
        }

        public AppUserContext(string dbConnectionString)
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
