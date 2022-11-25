namespace CW_1711_QuestRoom_.Models
{
    public class PhoneNumber
    {
        public int Id { get; set; }
        public int QRoomId { get; set; }
        public string Number { get; set; }

        public QRoom QRoom { get; set; }
    }
}
