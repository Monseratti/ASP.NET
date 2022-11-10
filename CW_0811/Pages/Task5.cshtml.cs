using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CW_0811.Pages
{
    public class Task5Model : PageModel
    {
		[BindProperty(SupportsGet = true)]
		public string? Name { get; set; }
		[BindProperty(SupportsGet = true)]
		public string? Phone { get; set; }
		[BindProperty(SupportsGet = true)]
		public string? Message { get; set; }

        public void OnGet()
        {
        }
    }
}
