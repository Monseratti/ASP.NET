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

        public List<QRoom> FilteredQrooms { get; set; } = new();

        public IndexModel(MyAppContext context)
        {
            db = context;
            FilteredQrooms = QRooms = db.Rooms.AsNoTracking().ToList();
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostFilterAsync(int minRating, int maxRating, int minFear, int maxFear, int minPeople, int maxPeople) 
        {
            if (minRating != 0 && minFear != 0 && minPeople != 0)
                FilteredQrooms = await db.Rooms.Where(o => o.Rating >= minRating && o.Rating <= maxRating
                                                      && o.HorrorLevel >= minFear && o.HorrorLevel <= maxFear
                                                      && o.MinNumOfPlayers >= minPeople && o.MaxNumOfPlayers <= maxPeople).AsNoTracking().ToListAsync();
            else if (minFear != 0 && minPeople != 0)
                FilteredQrooms = await db.Rooms.Where(o => o.HorrorLevel >= minFear && o.HorrorLevel <= maxFear
                                                      && o.MinNumOfPlayers >= minPeople && o.MaxNumOfPlayers <= maxPeople).AsNoTracking().ToListAsync();
            else if (minRating != 0 && minPeople != 0)
                FilteredQrooms = await db.Rooms.Where(o => o.Rating >= minRating && o.Rating <= maxRating
                                                       && o.MinNumOfPlayers >= minPeople && o.MaxNumOfPlayers <= maxPeople).AsNoTracking().ToListAsync();
            else if (minRating != 0 && minFear != 0)
                FilteredQrooms = await db.Rooms.Where(o => o.Rating >= minRating && o.Rating <= maxRating
                                                      && o.HorrorLevel >= minFear && o.HorrorLevel <= maxFear).AsNoTracking().ToListAsync();
            else if (minRating != 0)
                FilteredQrooms = await db.Rooms.Where(o => o.Rating >= minRating && o.Rating <= maxRating).AsNoTracking().ToListAsync();
            else if (minFear != 0)
                FilteredQrooms = await db.Rooms.Where(o => o.HorrorLevel >= minFear && o.HorrorLevel <= maxFear).AsNoTracking().ToListAsync();
            else if (minPeople != 0)
                FilteredQrooms = await db.Rooms.Where(o => o.MinNumOfPlayers >= minPeople && o.MaxNumOfPlayers <= maxPeople).AsNoTracking().ToListAsync();
            else 
                FilteredQrooms = QRooms;

            return Page();
        }

        public async Task<IActionResult> OnPostClearFilterAsync()
        {
            FilteredQrooms = QRooms;
            return Page();
        }
    }
}