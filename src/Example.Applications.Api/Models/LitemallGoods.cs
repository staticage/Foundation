using System;
using System.Collections.Generic;

namespace Example.Applications.Api.Models
{
    public partial class LitemallGoods
    {
        public int Id { get; set; }
        public string GoodsSn { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public string Gallery { get; set; }
        public string Keywords { get; set; }
        public string Brief { get; set; }
        public bool? IsOnSale { get; set; }
        public short? SortOrder { get; set; }
        public string PicUrl { get; set; }
        public string ShareUrl { get; set; }
        public bool? IsNew { get; set; }
        public bool? IsHot { get; set; }
        public string Unit { get; set; }
        public decimal? CounterPrice { get; set; }
        public decimal? RetailPrice { get; set; }
        public string Detail { get; set; }
        public DateTime? AddTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? Deleted { get; set; }
    }
}
