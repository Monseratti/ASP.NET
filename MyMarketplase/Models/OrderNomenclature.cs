using System.Data.SqlTypes;

namespace MyMarketplase.Models
{
    public class OrderNomenclature
    {
        public int Id { get; set; }
        public int OrderID { get; set; }
        public int NomID { get; set; }
        public decimal SellAmount { get; set; }
        public decimal SellingPrice { get; set; }

        public virtual Order Order { get; set; }
        public virtual Nomenclature Nomenclature { get; set; }
    }
}
