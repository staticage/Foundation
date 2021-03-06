﻿using System;
using System.Collections.Generic;

namespace Example.Applications.Api.Models
{
    public partial class LitemallCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Keywords { get; set; }
        public string Desc { get; set; }
        public int Pid { get; set; }
        public string IconUrl { get; set; }
        public string PicUrl { get; set; }
        public string Level { get; set; }
        public sbyte? SortOrder { get; set; }
        public DateTime? AddTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? Deleted { get; set; }
    }
}
