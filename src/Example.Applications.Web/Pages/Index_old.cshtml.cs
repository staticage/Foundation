using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Example.Applications.Web.Pages
{
    public class Index : PageModel
    {
        public string Title { get; set; }

        public Index()
        {
            Title = "Test title";
        }
        
        public void OnGet()
        {
            
        }
    }
}