using Piranha.AttributeBuilder;
using Piranha.Models;

namespace Example.Applications.Web.Models
{
    [PageType(Title = "Blog archive", UseBlocks = false, IsArchive = true)]
    public class BlogArchive : Page<BlogArchive>
    {
        /// <summary>
        /// Gets/sets the archive.
        /// </summary>
        public PostArchive<DynamicPost> Archive { get; set; }
    }
}