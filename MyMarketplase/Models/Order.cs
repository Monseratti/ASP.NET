namespace MyMarketplase.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int OrderStateID { get; set; }
        public int UserID { get; set; }


        public virtual OrderState OrderState { get; set; }
        public virtual User User { get; set; }
    }
}
