var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => $"{DateTime.Today.DayOfYear}");

//app.MapGet("/", async (context) =>
//{
//    Random random = new Random();
//    await context.Response.WriteAsync(((char)random.Next(65, 91)).ToString());
//});

//app.Run(async (context) =>
//{
//    context.Response.ContentType = "text/html";
//    switch (context.Request.Path)
//    {
//        case "/rest": 
//            await context.Response.SendFileAsync("html/restraunt.html"); 
//            break;
//        case "/rest1":
//            await context.Response.SendFileAsync("html/restraunt1.html");
//            break;
//        case "/rest2":
//            await context.Response.SendFileAsync("html/restraunt2.html");
//            break;
//        default:
//            await context.Response.SendFileAsync("html/index.html");
//            break;
//    }
    
//});

app.Run(async (context) =>
{
    context.Response.ContentType = "text/html";
    switch (context.Request.Path)
    {
        case "/ua":
            context.Response.Redirect("https://uk.wikipedia.org/wiki/%D0%A3%D0%BA%D1%80%D0%B0%D1%97%D0%BD%D0%B0");
            break;
        case "/uk":
            context.Response.Redirect("https://en.wikipedia.org/wiki/United_Kingdoml");
            break;
        case "/us":
            context.Response.Redirect("https://en.wikipedia.org/wiki/United_States");
            break;
        default:
            await context.Response.SendFileAsync("html/index1.html");
            break;
    }

});

app.Run();
