using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HW_0811_HW1_.Pages
{
    public class Task1_1Model : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult OnPostSubscribe(string email, bool check)
        {
            if(check)  return RedirectToPage("Index");
            else return Page();
        }
    }
}
