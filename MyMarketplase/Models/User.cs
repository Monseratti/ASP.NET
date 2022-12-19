using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace MyMarketplase.Models
{
    public class User
    {
        [ValidateNever]
        public int Id { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
        [ValidateNever]
        public ICollection<Order> Orders { get; set; }

        [ValidateNever]
        public virtual Role Role { get; set; }
    }
}
