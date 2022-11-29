using CW_1711_QuestRoom_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CW_1711_QuestRoom_.Pages
{
    public class EditRoomModel : PageModel
    {
        MyAppContext db;

        [BindProperty]
        public QRoom? QRoom { get; set; }

        public EditRoomModel (MyAppContext db)
        {
            this.db = db;
            QRoom= new QRoom ();
        }

        public async Task OnGetAsync(int id)
        {
            QRoom = await db.Rooms.FindAsync(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var oldRoom = await db.Rooms.Where(o => o.Id.Equals(QRoom!.Id)).AsNoTracking().FirstAsync();
            if (QRoom!.LogoPath == null) QRoom.LogoPath = oldRoom.LogoPath;
            else
            {
                var files = Request.Form.Files;
                var directory = $@"{Directory.GetCurrentDirectory()}\wwwroot\Files\{QRoom!.Name.Replace(" ", "_")}";
                Directory.CreateDirectory(directory);
                if (files.Count != 0)
                {
                    using(FileStream fs = new FileStream($@"{Directory.GetCurrentDirectory()}\wwwroot\{oldRoom.LogoPath}", FileMode.Truncate))
                    {
                        await fs.FlushAsync();
                    }
                    QRoom.LogoPath = $@"{directory}\{files[0].FileName}";
                    using (FileStream fs = new FileStream(QRoom.LogoPath, FileMode.Create))
                    {
                        await files[0].CopyToAsync(fs);
                    }
                    QRoom.LogoPath = $@"{directory}\{files[0].FileName}".Split("wwwroot")[1];
                }
            }
            db.Rooms.Update(QRoom!);
            await db.SaveChangesAsync();
            return RedirectToPage("Admin");
        }
    }
}
