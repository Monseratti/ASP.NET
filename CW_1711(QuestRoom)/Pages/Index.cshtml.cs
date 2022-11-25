using CW_1711_QuestRoom_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CW_1711_QuestRoom_.Pages
{
    public class IndexModel : PageModel
    {
        MyAppContext db;
        public List<QRoom> QRooms { get; set; } = new();

        public IndexModel(MyAppContext context)
        {
            db = context;
        }

        public void OnGet()
        {
            QRooms = db.Rooms.AsNoTracking().ToList();
        }
    }
}