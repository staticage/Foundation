using System;
using System.Collections.Generic;

namespace Example.Applications.Api.Models
{
    public partial class LitemallFootprint
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GoodsId { get; set; }
        public DateTime? AddTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? Deleted { get; set; }
    }
}
