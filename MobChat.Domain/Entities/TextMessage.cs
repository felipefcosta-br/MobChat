using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobChat.Domain.Entities
{
    [Table("TextMessages")]
    public class TextMessage : Message
    {
        public String Message { get; set; }
    }
}
