using CW_1711_QuestRoom_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CW_1711_QuestRoom_.Pages
{
    public class AddRoomModel : PageModel
    {
        MyAppContext db;
        [BindProperty]
        public QRoom? QRoom { get; set; }
        [BindProperty]
        public PhoneNumber? Number { get; set; }
        [BindProperty]
        public RoomEmail? RoomEmail { get; set; }
        [BindProperty]
        public PicturePath? Picture { get; set; }

        List<PhoneNumber>? PhoneNumbers { get; set; }
        List<RoomEmail>? RoomEmails { get; set; }
        List<PicturePath>? PicturePaths { get; set; }

        public AddRoomModel(MyAppContext db)
        {
            this.db = db;

            QRoom = new QRoom();
            Number = new PhoneNumber();
            RoomEmail = new RoomEmail();

            PhoneNumbers = new List<PhoneNumber>();
            RoomEmails = new List<RoomEmail>();
            PicturePaths = new List<PicturePath>();
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var files = Request.Form.Files;
            var directory = $@"{Directory.GetCurrentDirectory()}\wwwroot\Files\{QRoom!.Name.Replace(" ","_")}";
            Directory.CreateDirectory(directory);
            if (files.Count != 0)
            {
                QRoom.LogoPath = $@"{directory}\{files[0].FileName}";
                using (FileStream fs = new FileStream(QRoom.LogoPath, FileMode.Create))
                {
                    await files[0].CopyToAsync(fs);
                }
                QRoom.LogoPath = $@"{directory}\{files[0].FileName}".Split("wwwroot")[1];
            }
            await db.Rooms.AddAsync(QRoom!);
            await db.SaveChangesAsync();

            Number!.QRoomId = QRoom.Id;
            RoomEmail!.QRoomId = QRoom.Id;

            PhoneNumbers!.Add(Number);
            RoomEmails!.Add(RoomEmail);
            for (int i = 0; i < 100; i++)
            {
                if (Request.Form[$"phoneNumber{i}"].ToString() != "")
                {
                    Number = new PhoneNumber();
                    Number.QRoomId = QRoom.Id;
                    Number.Number = Request.Form[$"phoneNumber{i}"]!;
                    PhoneNumbers.Add(Number);
                }
                else
                {
                    break;
                }
            }
            for (int i = 0; i < 100; i++)
            {
                if (Request.Form[$"email{i}"].ToString() != "")
                {
                    RoomEmail = new RoomEmail();
                    RoomEmail.QRoomId = QRoom.Id;
                    RoomEmail.Email = Request.Form[$"email{i}"]!;
                    RoomEmails.Add(RoomEmail);
                }
                else
                {
                    break;
                }

            }
            if (files.Count > 1)
            {
                for (int i = 1; i < 100; i++)
                {
                    try
                    {
                        Picture = new PicturePath();
                        Picture.QRoomId = QRoom.Id;
                        var galleryDir = $@"{Directory.GetCurrentDirectory()}\wwwroot\Files\{QRoom!.Name.Replace(" ","_")}\Gallery";
                        Directory.CreateDirectory(galleryDir);
                        Picture.Path = $@"{galleryDir}\{files[i].FileName}";
                        using (FileStream fs = new FileStream(Picture.Path, FileMode.Create))
                        {
                            await files[i].CopyToAsync(fs);
                        }
                        Picture.Path = $@"{galleryDir}\{files[i].FileName}".Split("wwwroot")[1];
                        PicturePaths!.Add(Picture);
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
            }
            if (PicturePaths!.Count > 0) await db.AddRangeAsync(PicturePaths);
            await db.PhoneNumber.AddRangeAsync(PhoneNumbers);
            await db.RoomEmail.AddRangeAsync(RoomEmails);

            await db.SaveChangesAsync();

            return RedirectToPage("Index");
        }

    }
}
