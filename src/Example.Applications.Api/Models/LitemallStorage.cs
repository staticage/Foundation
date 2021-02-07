using System;
using System.Collections.Generic;

namespace Example.Applications.Api.Models
{
    public partial class LitemallStorage
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Size { get; set; }
        public string Url { get; set; }
        public DateTime? AddTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? Deleted { get; set; }
    }
}
