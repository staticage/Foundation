﻿using System;
using System.Collections.Generic;

namespace Example.Applications.Api.Models
{
    public partial class LitemallCart
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? GoodsId { get; set; }
        public string GoodsSn { get; set; }
        public string GoodsName { get; set; }
        public int? ProductId { get; set; }
        public decimal? Price { get; set; }
        public short? Number { get; set; }
        public string Specifications { get; set; }
        public bool? Checked { get; set; }
        public string PicUrl { get; set; }
        public DateTime? AddTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? Deleted { get; set; }
    }
}
