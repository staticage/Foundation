using Microsoft.AspNetCore.Mvc;

namespace Example.Applications.Api.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("api/test")]
        public object Test()
        {
            return "123";
        }
    }
}