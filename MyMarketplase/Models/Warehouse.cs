using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Data.SqlTypes;

namespace MyMarketplase.Models
{
    public class Warehouse
    {
        [ValidateNever]
        public int Id { get; set; }
        public string Name { get; set; }

        [ValidateNever]
        public ICollection <WarehouseNomenclature> WarehouseNoms { get; set; }
    }
}
