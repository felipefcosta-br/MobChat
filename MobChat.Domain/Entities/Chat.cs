using MobChat.Common.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobChat.Domain.Entities
{
    public class Chat : EntityBase<Guid>
    {       
        public Guid UserId { get; set; }
        public Guid ContactId { get; set; }
        public String ContactName { get; set; }
        public String ContactPhoto { get; set; }
        public String Status { get; set; }
        public bool Online { get; set; }
    }
}
