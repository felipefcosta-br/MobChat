using MobChat.Common.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobChat.UserMicroservice.Domain.AggregatesModel.UserAggregate
{
    public class AppUser : EntityBase<Guid>
    {
        public Guid AccountId { get; set; }
        public String Name { get; set; }
        public String LastName { get; set; }
        public String UserName { get; set; }
        public String Photo { get; set; }
        public String Thumbnail { get; set; }
        public String Email { get; set; }
        public String MobileNumber { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Country { get; set; }
        public DateTime Registration { get; set; }
    }
}
