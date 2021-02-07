using System;
using System.Collections.Generic;

namespace Example.Applications.Api.Models
{
    public partial class LitemallComment
    {
        public int Id { get; set; }
        public int ValueId { get; set; }
        public sbyte Type { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public bool? HasPicture { get; set; }
        public string PicUrls { get; set; }
        public short? Star { get; set; }
        public DateTime? AddTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? Deleted { get; set; }
    }
}
