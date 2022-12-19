using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MyMarketplase.Models
{
    public class WarehouseNomenclature
    {
        [ValidateNever]
        public int Id { get; set; }
        public int WarehouseID { get; set; }
        public int NomID { get; set; }
        public decimal NomAmount { get; set; }
        public decimal NomPrice { get; set; }

        [ValidateNever]
        public virtual Nomenclature Nomenclature { get; set; }
        [ValidateNever]
        public virtual Warehouse Warehouse { get; set; }
    }
}
