﻿using System;
using System.Collections.Generic;

namespace Example.Applications.Api.Models
{
    public partial class LitemallRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public bool? Enabled { get; set; }
        public DateTime? AddTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? Deleted { get; set; }
    }
}
