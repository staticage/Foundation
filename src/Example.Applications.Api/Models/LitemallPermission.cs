using System;
using System.Collections.Generic;

namespace Example.Applications.Api.Models
{
    public partial class LitemallPermission
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public string Permission { get; set; }
        public DateTime? AddTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? Deleted { get; set; }
    }
}
