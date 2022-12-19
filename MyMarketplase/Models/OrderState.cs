using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MyMarketplase.Models
{
    public class OrderState
    {
        [ValidateNever]
        public int Id { get; set; }
        public string Name { get; set; }
        [ValidateNever]
        public IEnumerable<Order> Orders { get;set; }
    }
}
