using System;
using System.Collections.Generic;

namespace Example.Applications.Api.Models
{
    public partial class LitemallAddress
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string AddressDetail { get; set; }
        public string AreaCode { get; set; }
        public string PostalCode { get; set; }
        public string Tel { get; set; }
        public bool IsDefault { get; set; }
        public DateTime? AddTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? Deleted { get; set; }
    }
}
