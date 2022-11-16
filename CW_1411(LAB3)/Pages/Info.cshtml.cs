using CW_1411_LAB3_.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Reflection.Metadata.BlobBuilder;
using System.Text.Json;

namespace CW_1411_LAB3_.Pages
{
    public class InfoModel : PageModel
    {
        public List<Book> Books= new List<Book>();

        public InfoModel()
        {
			using (StreamReader sr = new StreamReader("libr.json"))
			{
				Books = JsonSerializer.Deserialize<List<Book>>(sr.ReadToEnd())!;
			}
		}

        public void OnGet(string id)
        {
			Books = Books.Where(o=>o.id!.Equals(id)).ToList();
		}
    }
}
