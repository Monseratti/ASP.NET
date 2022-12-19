using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Data.SqlTypes;

namespace MyMarketplase.Models
{
    public class Nomenclature
    {
        [ValidateNever]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        
        [ValidateNever]
        public ICollection<FilesPath> Files { get; set; }
        [ValidateNever]
        public ICollection<OrderNomenclature> SellingNoms { get; set; }
        [ValidateNever]
        public ICollection<WarehouseNomenclature> WarehouseNoms { get; set; }
    }
}
