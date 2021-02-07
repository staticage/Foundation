using System;
using System.Collections.Generic;
using System.Text;
using Foundation.DDD;

namespace Example.Applications.Domain
{
    public class User : Entity<int>
    {
        public string Phone { get; set; }
        public string OpenId { get; set; }
        public string Avatar { get; set; }

        public static User Register(string phone)
        {
            return new User
            {
                Phone = phone
            };
        }

        public void BindWechat(string openId)
        {
            OpenId = openId;
        }
    }
}
