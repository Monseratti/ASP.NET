using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CW_1011.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		public List<Category> Categories { get; set; }
        public List<Category> SearchCategories { get; set; } = new();

        public List<Product> Products { get; set; }
		public List<Product> SearchProducts { get; set; } = new();

		[BindProperty(SupportsGet = true)]
		public string? ProductName { get; set; }

		[BindProperty(SupportsGet = true)]
		public string? ProductPrice { get; set; }

		[BindProperty(SupportsGet = true)]
		public string? CategoriesName { get; set; }

		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
			try
			{
				Products = JsonConvert.DeserializeObject<List<Product>>(ReadFile("products.json"))!;
			}
			catch (Exception)
			{
				Products = new List<Product>();
			}
			try
			{
				Categories = JsonConvert.DeserializeObject<List<Category>>(ReadFile("categories.json"))!;
			}
			catch (Exception)
			{
				Categories = new List<Category>();
			}

		}

		public void OnGet() { }
		public void OnPostAdded()
		{
			Products.Add(new Product(ProductName!, Decimal.Parse(ProductPrice!)));
			saveFile(Products, "products.json");
		}
		public void OnPostSearch()
		{
			SearchProducts = Products.Where(o => o.Name.ToLower().Contains(ProductName!.ToLower())).ToList();
		}

		public void OnPostCreateCategory()
		{
			if (CategoriesName == null || CategoriesName == "") return;
			List<Product> tmpCheck = new List<Product>();
			foreach (Product product in Products.Where(o => o.Category == null).ToList())
			{
				try
				{
					tmpCheck.Add(Products.Where(o => o.Name.Equals(Request.Form[product.Name])).First());

				}
				catch (Exception)
				{

				}
			}
			if (tmpCheck.Count > 0)
			{
				Categories.Add(new Category(CategoriesName!, tmpCheck));

				foreach (Product product in Categories.Last().getCategoryProduct())
				{
					product.Category = Categories.Last().getCategoryName();
				}

				saveFile(Categories, "categories.json");
				saveFile(Products, "products.json");
			}
		}
		public void OnPostSearchCategory()
		{
			SearchCategories = Categories.Where(c => c.getCategoryName().Equals(Request.Form["selectCategory"])).ToList();
        }




        string ReadFile(string fileName)
		{
			string json = "";
			using (StreamReader reader = new StreamReader(fileName))
			{
				json = reader.ReadToEnd();
			}
			return json;
		}
		async void saveFile(object obj, string fileName)
		{
			string json = "";
			
			if (obj is List<Product>)
			{
				json = JsonConvert.SerializeObject(obj as List<Product>);
			}
			else if (obj is List<Category>)
			{
				json = JsonConvert.SerializeObject(obj);
			}
			using (StreamWriter writer = new StreamWriter(fileName, false))
			{
				await writer.WriteLineAsync(json);
			}
		}

	}
}