using System;
using System.Collections.Generic;

namespace Example.Applications.Api.Models
{
    public partial class LitemallSystem
    {
        public int Id { get; set; }
        public string KeyName { get; set; }
        public string KeyValue { get; set; }
        public DateTime? AddTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? Deleted { get; set; }
    }
}
