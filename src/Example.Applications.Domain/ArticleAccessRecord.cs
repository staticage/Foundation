using System;
using System.Collections.Generic;
using System.Text;
using Foundation.DDD;

namespace Example.Applications.Domain
{
    public class ArticleAccessRecord : Entity<int>
    {
        public Article Article { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public Visitor Visitor { get; set; }
        public string Forwarder { get; set; }
    }

    public class Visitor : Entity<int>
    {
        public string OpenId { get; set; }
    }
}
