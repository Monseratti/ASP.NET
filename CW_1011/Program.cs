using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();


public class Product
{
	public string Name { get; set; }
	public decimal Price { get; set; }
	public string? Category { get; set; }

	public Product(string name, decimal price, string? category = null)
	{
		Name = name;
		Price = price;
		Category = category;
	}

	public override string ToString()
	{
		if (Category != null)
			 return $"Name: {Name}, price: {Price}, category: {Category}";
		else return $"Name: {Name}, price: {Price}";
	}
}

public class Category
{
	public string Name;
	public List<Product> Products;
	
	public Category(string name, List<Product> products)
	{
		Name = name; Products = products;
	}
	public string getCategoryName()
	{
		return Name;
	}
	public List<Product> getCategoryProduct()
	{
		return Products;
	}
}