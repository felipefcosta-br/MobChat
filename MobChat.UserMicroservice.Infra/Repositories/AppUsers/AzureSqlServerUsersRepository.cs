﻿using Microsoft.EntityFrameworkCore;
using MobChat.Common.Infra.DataAccess.Repositories;
using MobChat.UserMicroservice.Domain.AggregatesModel.UserAggregate;
using MobChat.UserMicroservice.Infra.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.UserMicroservice.Infra.Repositories.AppUsers
{
    public class AzureSqlServerUsersRepository : EntityFrameworkRepositoryBase<Guid, AppUser>, IAppUserRepository
    {

        public AzureSqlServerUsersRepository()
            : base(new AppUserContext("Server=tcp:mobchat-user-db-server.database.windows.net,1433;Initial Catalog=mobchat-usermicroservice-db;Persist Security Info=False;User ID=felipefcrj;Password=00Xam-Project-BR-77;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
        {
        }
        public async Task<AppUser> FindByAccountIdAsync(Guid accountId)
        {
            return await dbContext.Set<AppUser>().FirstOrDefaultAsync(user => user.AccountId == accountId);
        }

        public async Task<AppUser> GetAppUserByUserName(string userName)
        {
            return await dbContext.Set<AppUser>().FirstOrDefaultAsync(user => user.UserName == userName);
        }

        public IEnumerable<AppUser> SearchForUsers(string searchTxt)
        {
            var result = dbContext.Set<AppUser>().Where(user => user.Name.ToLower().Contains(searchTxt.Trim().ToLower()) ||
                                                  user.UserName.ToLower().Contains(searchTxt.Trim().ToLower())).AsEnumerable();
            return result;
        }
    }
}
