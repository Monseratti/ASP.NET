using CW_0511.Classes;
using System.Text.RegularExpressions;

List<Room> rooms = new List<Room>();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (context) =>
{
    string regExEdit = @"^/edit/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}$";
    string regExDetele = @"^/delete/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}$";
    var request = context.Request;
    var path = request.Path;
    var method = request.Method;
    var response = context.Response;
    //all
    if (path == "/room" && method == "GET")
    {
        await getAllRooms(response);
    }
    //create
    else if (path == "/create" && method == "POST")
    {
        //await createRoom(request, response);
        response.Redirect("/room");

    }
    else if (path == "/create")
    {
        await response.SendFileAsync("HTML/create.html");
    }
    //read
    else if (Regex.IsMatch(path, regExEdit) && method == "GET")
    {

    }
    //update
    else if (path == "/edit" && method == "PUT")
    {

    }
    //delete
    else if (Regex.IsMatch(path, regExDetele) && method == "DELETE")
    {

    }
    //else
    else
    {
        context.Response.Headers.Remove("Content-Length");
        response.ContentType = "text/html; charset = utf-8";
        await response.SendFileAsync("HTML/index.html");
    }
    //send response
});



app.Run();


async Task getAllRooms(HttpResponse response)
{
    response.Redirect("/home",false);
    await response.WriteAsJsonAsync(rooms);
}

async Task createRoom(HttpRequest request, HttpResponse response)
{
    Room? room = await request.ReadFromJsonAsync<Room>();
    if (room != null)
    {
        room.Id = Guid.NewGuid().ToString();
        rooms.Add(room);
        await response.WriteAsJsonAsync(new { saved = true });
    }
}