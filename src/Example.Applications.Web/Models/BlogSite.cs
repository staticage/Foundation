using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Models;

namespace Example.Applications.Web.Models
{
    [SiteType(Title = "Default site")]
    public class BlogSite : SiteContent<BlogSite>
    {
        [Region]
        public Regions.SiteInfo Information { get; set; }
    }
}