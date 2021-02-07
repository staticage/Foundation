using System;
using System.Collections.Generic;

namespace Example.Applications.Api.Models
{
    public partial class LitemallCollect
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ValueId { get; set; }
        public sbyte Type { get; set; }
        public DateTime? AddTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? Deleted { get; set; }
    }
}
