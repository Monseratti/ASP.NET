using CW_1411_LAB3_.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace CW_1411_LAB3_.Pages
{
    public class EditModel : PageModel
    {
		List<Book> Books = new List<Book>();
		public Book Book { get; set; } = new();
        public EditModel() 
        {
			using (StreamReader sr = new StreamReader("libr.json"))
			{
				Books = JsonSerializer.Deserialize<List<Book>>(sr.ReadToEnd())!;
			}
		}
		public void OnGet(string id)
        {
			Book = Books.Find(o => o.id!.Equals(id))!;
		}

        public async Task<IActionResult> OnPostAsync(string id, string bookName, string authorName, string bookStyle, string publName, string bookYear)
        {
            Books.Find(o => o.id!.Equals(id))!.Name = bookName;
            Books.Find(o => o.id!.Equals(id))!.Author = authorName;
            Books.Find(o => o.id!.Equals(id))!.Style = bookStyle;
            Books.Find(o => o.id!.Equals(id))!.Publisher = publName;
            Books.Find(o => o.id!.Equals(id))!.Year = int.Parse(bookYear);

            try
            {
                using (StreamWriter sw = new StreamWriter("libr.json"))
                {
                    await sw.WriteLineAsync(JsonSerializer.Serialize(Books));
                }
                return RedirectToPage("Index");
            }
            catch (Exception)
            {
                return Page();
            }
		}
    }
}
