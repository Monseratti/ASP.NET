using CW_1711_QuestRoom_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CW_1711_QuestRoom_.Pages
{
    public class QRoomInfoModel : PageModel
    {
        MyAppContext db;

        public QRoom? QRoom { get; set; } = new();
        public List<RoomEmail> Emails { get; set; } = new();
        public List<PhoneNumber> Numbers { get; set; } = new();
        public List<PicturePath> PicturePaths { get; set; } = new();

        public QRoomInfoModel(MyAppContext db)
        {
            this.db = db;
        }

        public async Task OnGetAsync(int id)
        {
            QRoom = await db.Rooms.FindAsync(id);
            Emails = await db.RoomEmail.Where(o => o.QRoomId.Equals(id)).ToListAsync();
            Numbers = await db.PhoneNumber.Where(o => o.QRoomId.Equals(id)).ToListAsync();
			PicturePaths = await db.PicturePath.Where(o => o.QRoomId.Equals(id)).ToListAsync();
		}
    }
}
