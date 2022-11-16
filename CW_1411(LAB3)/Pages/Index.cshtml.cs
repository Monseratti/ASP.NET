using CW_1411_LAB3_.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace CW_1411_LAB3_.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<Book> Books { get; private set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            Books = new List<Book>();
            try
            {
                using (StreamReader sr = new StreamReader("libr.json"))
                {
                    Books = JsonSerializer.Deserialize<List<Book>>(sr.ReadToEnd())!;
                }
            }
            catch (Exception)
            {
            }
        }

        public void OnGet()  { }

        public IActionResult OnPostInfo(string id) 
        {
            return RedirectToPage("Info", new { id });
        }
		public IActionResult OnPostEdit(string id)
		{
			return RedirectToPage("Edit", new { id });
		}
		public IActionResult OnPostDelete(string id)
		{
			try
			{
                Books.Remove(Books.Find(o => o.id!.Equals(id))!);
				using (StreamWriter sw = new StreamWriter("libr.json"))
				{
					sw.WriteLine(JsonSerializer.Serialize(Books));
				}
                return RedirectToPage("Index");
			}
			catch (Exception)
			{
				return RedirectToPage("Index");
			}
		}

	}
}