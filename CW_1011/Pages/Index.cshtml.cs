using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CW_1011.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		public List<Product> Products { get; set; }
		public List<Product> SearchProducts { get; set; } = new();

		[BindProperty(SupportsGet = true)]
		public string? ProductName { get; set; }

		[BindProperty(SupportsGet = true)]
		public string? ProductPrice { get; set; }

		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
			try
			{
                Products = JsonConvert.DeserializeObject<List<Product>>(ReadFile())!;
            }
			catch (Exception)
			{
                Products = new List<Product>();
            }
			
		}

		public void OnGet() { }
		public void OnPostAdded()
		{
			Products.Add(new Product(ProductName!, Decimal.Parse(ProductPrice!)));
			saveProduct();
        }
		public void OnPostSearch()
		{
			SearchProducts = Products.Where(o => o.Name.ToLower().Contains(ProductName!.ToLower())).ToList();
		}

		string ReadFile()
		{
			string json = "";
			using (StreamReader reader = new StreamReader("Products.json"))
			{
				json = reader.ReadToEnd();
            }
			return json;
		}
        async void saveProduct()
        {
            string jsonProducts = JsonConvert.SerializeObject(Products);
            using (StreamWriter writer = new StreamWriter("Products.json", false))
            {
                await writer.WriteLineAsync(jsonProducts);
            }
        }

    }
}