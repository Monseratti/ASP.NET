using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CW_0811.Pages
{
    public class Task1Model : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public string? SomeText { get; set; }

        public void OnGet()
        {
        }

    }
}
