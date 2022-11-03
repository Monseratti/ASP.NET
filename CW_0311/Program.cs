using CW_0311;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

List<Auto> autos = new List<Auto>() {
    new Auto(){ Id = Guid.NewGuid().ToString(), Color = "red", Mark = "Bougatti", Model = "Veyron", Year = 2022 },
    new Auto(){ Id = Guid.NewGuid().ToString(), Color = "blue", Mark = "Yaguar", Model = "01", Year = 2022 },
    new Auto(){ Id = Guid.NewGuid().ToString(), Color = "green", Mark = "BMW", Model = "X5", Year = 2022 }
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.Run(async (context) =>
{
    string regEx = @"^/api/autos/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}$";
    var request = context.Request;
    var path = request.Path;
    var method = request.Method;
    var response = context.Response;
    //all
    if (path == "/api/autos" && method == "GET")
    {
        await getAllAuto(response);
    }
    //create
    else if (path == "/api/autos" && method == "POST")
    {
        await createAuto(response, request);
    }
    //read
    else if (Regex.IsMatch(path,regEx) && method == "GET")
    {
        await getSomeAuto(response, request);
    }
    //update
    else if (path == "/api/autos" && method == "PUT")
    {
        await editAuto(response, request);
    }
    //delete
    else if (Regex.IsMatch(path, regEx) && method == "DELETE")
    {
        await deleteAuto(response, request);
    }
    //else
    else
    {
        response.ContentType = "text/html; charset = utf-8";
        await response.SendFileAsync("HTML/index.html");
    }
    //send response
});

async Task deleteAuto(HttpResponse response, HttpRequest request)
{
    var id = request.Query["id"];
    await response.WriteAsJsonAsync(new { deleted = autos.Remove(autos.Find(e => e.Id == id)!) });
}

app.Run();

async Task getAllAuto(HttpResponse httpResponse)
{
    await httpResponse.WriteAsJsonAsync(autos);
}

async Task createAuto(HttpResponse response, HttpRequest request)
{
    Auto? tmp = await request.ReadFromJsonAsync<Auto>();
    if(tmp != null)
    {
        tmp.Id = Guid.NewGuid().ToString();
        autos.Add(tmp);
        await response.WriteAsJsonAsync(tmp);
    }
    else
    {
        response.StatusCode = 400;
        await response.WriteAsJsonAsync(new { text = "Error data set!" });
    }
}

async Task getSomeAuto(HttpResponse response, HttpRequest request)
{
    var id = request.Query["id"];
    var auto = autos.Find(e => e.Id == id);
    await response.WriteAsJsonAsync(auto);
}

async Task editAuto(HttpResponse response, HttpRequest request)
{
    Auto? tmp = await request.ReadFromJsonAsync<Auto>();
    if (tmp != null)
    {
        autos.Find(e => e.Id == tmp.Id)!.Mark = tmp.Mark;
        autos.Find(e => e.Id == tmp.Id)!.Model = tmp.Model;
        autos.Find(e => e.Id == tmp.Id)!.Color = tmp.Color;
        autos.Find(e => e.Id == tmp.Id)!.Year = tmp.Year;
        await response.WriteAsJsonAsync(autos);
    }
}
