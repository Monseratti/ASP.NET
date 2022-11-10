using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CW_0811.Pages
{
    public class Task3_4_2Model : PageModel
    {
        [BindProperty, Required, MinLength(3)]
        public string? Login { get; set; }
		[BindProperty, Required, MinLength(6)]
		public string? Password { get; set; }
		[BindProperty, Required, Compare(nameof(Password))]
		public string? ConfPassword { get; set; }
		[BindProperty, RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
		public string? Email { get; set; }
		[BindProperty, RegularExpression(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$")]
		public string? Phone { get; set; }

		public void OnGet()
        {
        }
    }
}
