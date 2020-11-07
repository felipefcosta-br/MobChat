using MobChat.Common.Domain.Interface.Repositories;
using MobChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobChat.Domain.Interfaces.Repositories
{
    public interface IAppUserRepository : IRepository<Guid, AppUser>
    {
    }
}
