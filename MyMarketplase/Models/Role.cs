using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MyMarketplase.Models
{
    public class Role
    {
        [ValidateNever]
        public int Id { get; set; }
        public string Name { get; set; }
        [ValidateNever]
        public ICollection<User> Users { get; set; }
    }
}
