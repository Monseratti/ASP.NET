using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MyMarketplase.Models
{
    public class Order
    {
        [ValidateNever]
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int OrderStateID { get; set; }
        public int UserID { get; set; }

        [ValidateNever]
        public ICollection<OrderNomenclature> SellingNoms { get; set; }

        [ValidateNever]
        public virtual OrderState OrderState { get; set; }
        [ValidateNever]
        public virtual User User { get; set; }
    }
}
