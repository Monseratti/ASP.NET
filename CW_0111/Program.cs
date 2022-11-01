var builder = WebApplication.CreateBuilder();
var app = builder.Build();
//app.Run(async (context) =>
//{
//    context.Response.ContentType = "text/html; charset=utf-8";
//    var stringBuilder = new System.Text.StringBuilder($"Hello<br>");
//    await context.Response.WriteAsync(stringBuilder.ToString());
//    await Task.Delay(2000);
//    stringBuilder = new System.Text.StringBuilder($"{DateTime.Now}<br>");
//    await context.Response.WriteAsync(stringBuilder.ToString());
//    await Task.Delay(2000);
//    await context.Response.SendFileAsync("user1.json");
//});

app.Run(async (context) =>
{
    string message = "No user!";
    if (context.Request.Path == "/user")
    {
        context.Response.ContentType = "application/json; charset=utf-8";
        var user = await context.Request.ReadFromJsonAsync<User>();
        if (user != null)
        {
            message = $"User is: {user.Name} {user.Second_Name} {user.Surname}";
        }
        await context.Response.WriteAsJsonAsync(new { text = message });
    }
    else if (context.Request.Path == "/t5")
    {
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.SendFileAsync("t5.html");
    }
    else
    {
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.SendFileAsync("index.html");
    }
});

app.Run();
public record User(string Name, string Second_Name, string Surname);
