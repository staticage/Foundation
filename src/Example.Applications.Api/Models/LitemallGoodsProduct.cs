﻿using System;
using System.Collections.Generic;

namespace Example.Applications.Api.Models
{
    public partial class LitemallGoodsProduct
    {
        public int Id { get; set; }
        public int GoodsId { get; set; }
        public string Specifications { get; set; }
        public decimal Price { get; set; }
        public int Number { get; set; }
        public string Url { get; set; }
        public DateTime? AddTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? Deleted { get; set; }
    }
}
