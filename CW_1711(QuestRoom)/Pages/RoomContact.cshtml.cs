using CW_1711_QuestRoom_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CW_1711_QuestRoom_.Pages
{
    public class RoomContactModel : PageModel
    {
        MyAppContext db;
        public List<PhoneNumber> PhoneNumbers { get; set; }
        public List<RoomEmail> Emails { get; set; }

        public RoomContactModel(MyAppContext db)
        {
            this.db = db;
            PhoneNumbers = new List<PhoneNumber>();
            Emails = new List<RoomEmail>();
        }

        public async Task OnGetAsync(int id)
        {
            PhoneNumbers = await db.PhoneNumber.Where(o => o.QRoomId.Equals(id)).AsNoTracking().ToListAsync();
            Emails = await db.RoomEmail.Where(o => o.QRoomId.Equals(id)).AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            List<PhoneNumber> tmpPhone = new List<PhoneNumber>();
            List<RoomEmail> tmpEmail = new List<RoomEmail>();

            for (int i = 0; i < 100; i++)
            {
                if (Request.Form[$"number_{i}"].ToString() != "")
                {
                    tmpPhone.Add(new PhoneNumber
                    {
                        QRoomId = int.Parse(Request.Form["id"]!),
                        Number = Request.Form[$"number_{i}"]!
                    });
                }
                else
                {
                    break;
                }
            }

            for (int i = 0; i < 100; i++)
            {
                if (Request.Form[$"email_{i}"].ToString() != "")
                {
                    tmpEmail.Add(new RoomEmail
                    {
                        QRoomId = int.Parse(Request.Form["id"]!),
                        Email = Request.Form[$"email_{i}"]!
                    });
                }
                else
                {
                    break;
                }
            }

            db.PhoneNumber.RemoveRange(PhoneNumbers);
            db.RoomEmail.RemoveRange(Emails);

            await db.AddRangeAsync(tmpPhone);
            await db.AddRangeAsync(tmpEmail);

            await db.SaveChangesAsync();

            return RedirectToPage("Admin");
        }
    }
}
