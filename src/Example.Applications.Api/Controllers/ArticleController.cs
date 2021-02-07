using System.Threading.Tasks;
using Foundation.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace Example.Applications.Api.Controllers
{
    public class ArticleController : Controller
    {
        public Task Create()
        {

        }
    }

    public class CreateArticleCommand : ICommand
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Cover { get; set; }
        public string Content { get; set; }
    }
}
