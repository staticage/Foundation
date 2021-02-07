using System;
using System.Collections.Generic;

namespace Example.Applications.Api.Models
{
    public partial class LitemallKeyword
    {
        public int Id { get; set; }
        public string Keyword { get; set; }
        public string Url { get; set; }
        public bool IsHot { get; set; }
        public bool IsDefault { get; set; }
        public int SortOrder { get; set; }
        public DateTime? AddTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? Deleted { get; set; }
    }
}
