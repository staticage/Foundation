using System;
using System.Collections.Generic;
using System.Text;
using Foundation.DDD;

namespace Example.Applications.Domain
{
    public class Article : Entity<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public User Author { get; set; }
        public string Link { get; set; }
        public string Cover { get; set; }
        public string Content { get; set; }
    }
}
