using System;
using System.Collections.Generic;

namespace Example.Applications.Api.Models
{
    public partial class LitemallGroupon
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int? GrouponId { get; set; }
        public int RulesId { get; set; }
        public int UserId { get; set; }
        public string ShareUrl { get; set; }
        public int CreatorUserId { get; set; }
        public DateTime? CreatorUserTime { get; set; }
        public short? Status { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? Deleted { get; set; }
    }
}
