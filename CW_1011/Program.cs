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

	public Product(string name, decimal price)
	{
		Name = name;
		Price = price;
	}

	public override string ToString()
	{
		return $"Name: {Name}, price: ${Price}";
	}
}