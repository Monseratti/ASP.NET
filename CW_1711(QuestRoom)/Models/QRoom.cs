using System.ComponentModel.DataAnnotations;

namespace CW_1711_QuestRoom_.Models
{
    public class QRoom
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора.
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PassingTime { get; set; }
        public int MinNumOfPlayers { get; set; }
        public int MaxNumOfPlayers { get; set; }
        public int MinYearOfPlayer { get; set; }
        public string Address { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public ICollection<RoomEmail> Emails { get; set; }
        public string Company { get; set; }
        [Range(0, 5)]
        public int Rating { get; set; }
        [Range(0, 5)]
        public int HorrorLevel { get; set; }
        [Range(0, 5)]
        public int Difficulty { get; set; }
        public string LogoPath { get; set; }
        public ICollection<PicturePath> PicturePathes { get; set; }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора.
    }
}
