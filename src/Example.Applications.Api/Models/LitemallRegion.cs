using System;
using System.Collections.Generic;

namespace Example.Applications.Api.Models
{
    public partial class LitemallRegion
    {
        public int Id { get; set; }
        public int Pid { get; set; }
        public string Name { get; set; }
        public sbyte Type { get; set; }
        public int Code { get; set; }
    }
}
