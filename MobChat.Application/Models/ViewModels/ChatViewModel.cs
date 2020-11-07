using System;
using System.Collections.Generic;
using System.Text;

namespace MobChat.Application.Models.ViewModels
{
    public class ChatViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ContactId { get; set; }
        public String ContactName { get; set; }
        public String ContactPhoto { get; set; }
        public String Status { get; set; }
        public bool Online { get; set; }
    }
}
