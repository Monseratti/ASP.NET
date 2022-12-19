using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MyMarketplase.Models
{
    public class Category
    {
        [ValidateNever]
        public int Id { get; set; }
        public string Name { get; set; }
        [ValidateNever]
        public ICollection<Nomenclature> Nomenclatures { get; set; }
    }
}
