﻿using System;
using System.Collections.Generic;

namespace Example.Applications.Api.Models
{
    public partial class LitemallAdmin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string LastLoginIp { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string Avatar { get; set; }
        public DateTime? AddTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? Deleted { get; set; }
        public string RoleIds { get; set; }
    }
}
