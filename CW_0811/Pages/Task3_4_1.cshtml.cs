using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CW_0811.Pages
{
    public class Task3_4_1Model : PageModel
    {
        public bool? isConfirm { get; private set; }
        string Login = "Login";
        string Password = "pass";

        public void OnGet()
        {
        }
        public void OnPostConfirm(string login, string pass)
        {
            if (login == Login && pass == Password) isConfirm = true;
            else isConfirm = false;
        }
    }
}
