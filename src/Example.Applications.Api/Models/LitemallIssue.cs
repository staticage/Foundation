using System;
using System.Collections.Generic;

namespace Example.Applications.Api.Models
{
    public partial class LitemallIssue
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime? AddTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? Deleted { get; set; }
    }
}
