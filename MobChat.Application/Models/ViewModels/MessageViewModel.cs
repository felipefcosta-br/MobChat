using System;
using System.Collections.Generic;
using System.Text;

namespace MobChat.Application.Models.ViewModels
{
    public abstract class MessageViewModel
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public Guid SenderId { get; set; }
        public Guid ContactId { get; set; }
        public String ContactName { get; set; }
        public String ContactPhoto { get; set; }
        public Guid UserId { get; set; }
        public String UserName { get; set; }
        public DateTime? MessageDate { get; set; }
        public String Status { get; set; }
    }
}
