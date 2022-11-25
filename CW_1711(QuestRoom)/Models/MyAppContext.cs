using Microsoft.EntityFrameworkCore;

namespace CW_1711_QuestRoom_.Models
{
    public class MyAppContext : DbContext
    {
        public DbSet<QRoom> Rooms { get; set; }
        public DbSet<PhoneNumber> PhoneNumber { get; set; }
        public DbSet<RoomEmail> RoomEmail { get; set; }
        public DbSet<PicturePath> PicturePath { get; set; }

        public MyAppContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
