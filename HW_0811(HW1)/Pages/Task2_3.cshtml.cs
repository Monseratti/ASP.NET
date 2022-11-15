using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HW_0811_HW1_.Pages
{
    public class Task2_3Model : PageModel
    {
        public int Score { get; set; }

        public void OnGet(string score)
        {
            Score = int.Parse(score);
        }
        public IActionResult OnPost()
        {
            return RedirectToPage("Task2");
        }
    }
}
