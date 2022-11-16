using CW_1411_LAB3_.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace CW_1411_LAB3_.Pages
{
    public class AddBookModel : PageModel
    {
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync(string bookName, string authorName, string bookStyle, string publName, string bookYear) 
        {
            List<Book> Books = new List<Book>();
            try
            {
                using (StreamReader sr = new StreamReader("libr.json"))
                {
                    Books = JsonSerializer.Deserialize<List<Book>>(await sr.ReadToEndAsync())!;
                }
            }
            catch (Exception)
            {
            }
            Books.Add(new Book()
            {
                id = Guid.NewGuid().ToString(),
                Name = bookName,
                Author = authorName,
                Style = bookStyle,
                Publisher= publName,
                Year = int.Parse(bookYear)
            });
            try
            {
                using(StreamWriter sw = new StreamWriter("libr.json"))
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
