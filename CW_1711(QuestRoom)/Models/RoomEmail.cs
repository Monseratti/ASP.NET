namespace CW_1711_QuestRoom_.Models
{
    public class RoomEmail
    {
        public int Id { get; set; }
        public int QRoomId { get; set; }
        public string Email { get; set; }

        public QRoom QRoom { get; set; }
    }
}
