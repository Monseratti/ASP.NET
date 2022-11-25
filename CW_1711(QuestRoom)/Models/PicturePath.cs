namespace CW_1711_QuestRoom_.Models
{
    public class PicturePath
    {
        public int Id { get; set; }
        public int QRoomId { get; set; }
        public string Path { get; set; }

        public QRoom QRoom { get; set; }
    }
}
