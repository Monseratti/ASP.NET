using CW_0511_1.classes;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

List<Room>? rooms = new List<Room>();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;
    var path = context.Request.Path;
    var method = context.Request.Method;

    var regExFind = @"/find/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}";
    var regExDelete = @"/delete/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}";
    //view
    if (path == "/rooms" && method == "GET")
    {
        await viewRooms(response);
    }
    //create
    else if (path == "/create" && method == "POST")
    {
        await createRoom(request, response);
    }
    //get
    else if (Regex.IsMatch(path, regExFind) && method == "GET")
    {
        await findSomeRoom(request, response);
    }
    //update
    else if (path == "/update" && method == "POST")
    {
        await updateRoom(request, response);
    }
    //delete
    else if (Regex.IsMatch(path, regExDelete) && method == "DELETE")
    {
        await deleteRoom(request, response);
    }
    else
    {
        response.ContentType = "text/html";
        await response.SendFileAsync("html/index.html");
    }
});


app.Run();

async Task createRoom(HttpRequest request, HttpResponse response)
{
    Room room = new Room() { Name = request.Form["name"], Info = request.Form["info"], Id = Guid.NewGuid().ToString() };
    var directory = $@"{Directory.GetCurrentDirectory()}\images\{room.Name}";
    Directory.CreateDirectory(directory);
    if (request.Form.Files.Count != 0)
    {
        room.Bg_image = $@"{directory}\{request.Form.Files[0].FileName}";
        using (var fileStream = new FileStream(room.Bg_image, FileMode.Create))
        {
            await request.Form.Files[0].CopyToAsync(fileStream);
        }
    }
    else { room.Bg_image = null; }
    rooms.Add(room);
    response.Redirect("/", false);
    saveRooms();
}

async Task viewRooms(HttpResponse response)
{
    string? line;
    using (StreamReader reader = new StreamReader("rooms.json"))
    {
        line = await reader.ReadToEndAsync();
    }
    if (line != null) rooms = JsonConvert.DeserializeObject<List<Room>>(line);
    await response.WriteAsJsonAsync(rooms);
}

async Task findSomeRoom(HttpRequest request, HttpResponse response)
{
    await response.WriteAsJsonAsync(rooms.Find(o => o.Id == request.Query["id"]));
}

async Task updateRoom(HttpRequest request, HttpResponse response)
{
    rooms.Find(o => o.Id == request.Form["id"])!.Name = request.Form["name"];
    rooms.Find(o => o.Id == request.Form["id"])!.Info = request.Form["info"];
    var directory = $@"{Directory.GetCurrentDirectory()}\images\{rooms.Find(o => o.Id == request.Form["id"])!.Name}";
    Directory.CreateDirectory(directory);
    if (request.Form.Files.Count != 0)
    {
        rooms.Find(o => o.Id == request.Form["id"])!.Bg_image = $@"{directory}\{request.Form.Files[0].FileName}";
        using (var fileStream = new FileStream(rooms.Find(o => o.Id == request.Form["id"])!.Bg_image, FileMode.Create))
        {
            await request.Form.Files[0].CopyToAsync(fileStream);
        }
    }
    response.Redirect("/", false);
    saveRooms();
}

async Task deleteRoom(HttpRequest request, HttpResponse response)
{
    rooms.Remove(rooms.Find(o => o.Id == request.Query["id"]));
    saveRooms();
    response.Redirect("/", false);
    await response.WriteAsJsonAsync(new { deleted = true });
}

async void saveRooms()
{
    string jsonRooms = JsonConvert.SerializeObject(rooms);
    using (StreamWriter writer = new StreamWriter("rooms.json", false))
    {
        await writer.WriteLineAsync(jsonRooms);
    }
}
