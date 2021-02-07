using System;
using System.Collections.Generic;

namespace Example.Applications.Api.Models
{
    public partial class LitemallLog
    {
        public int Id { get; set; }
        public string Admin { get; set; }
        public string Ip { get; set; }
        public int? Type { get; set; }
        public string Action { get; set; }
        public bool? Status { get; set; }
        public string Result { get; set; }
        public string Comment { get; set; }
        public DateTime? AddTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? Deleted { get; set; }
    }
}
