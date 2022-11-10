using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CW_0811.Pages
{
    public class Task2Model : PageModel
    {
        [BindProperty]
        public string? Country { get; set; }
        public List<string> Countries = new List<string>()
            {
                "Canada", "Poland", "Portugal", "USA", "United Kindoms", "Ukraine"
            };
        public List<string> DisplayedCountries { get; set; } = new();

        public void OnGet()
        {
            DisplayedCountries = Countries;

		}
		public void OnPost()
		{
			DisplayedCountries = Countries;

		}
		public void OnPostBySelect(string country)
        {
            DisplayedCountries = Countries.Where(o => o.ToLower().Contains(country.ToLower())).ToList();
        }
    }
}
