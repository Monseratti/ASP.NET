using CW_1711_QuestRoom_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CW_1711_QuestRoom_.Pages
{
    public class AdminModel : PageModel
    {
        MyAppContext db;
        public List<QRoom> RoomList { get; set; }
        public AdminModel(MyAppContext db)
        {
            this.db = db;
        }

        public async Task OnGetAsync()
        {
            RoomList = await db.Rooms.AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            db.Rooms.Remove(await db.Rooms.FindAsync(id));
            db.RoomEmail.RemoveRange(await db.RoomEmail.Where(o=>o.QRoomId.Equals(id)).ToListAsync());
            db.PhoneNumber.RemoveRange(await db.PhoneNumber.Where(o=>o.QRoomId.Equals(id)).ToListAsync());
            db.PicturePath.RemoveRange(await db.PicturePath.Where(o=>o.QRoomId.Equals(id)).ToListAsync());
            await db.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
