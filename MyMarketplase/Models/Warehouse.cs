using System.Data.SqlTypes;

namespace MyMarketplase.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public int NomID { get; set; }
        public decimal NomAmount { get; set; }
        public decimal NomPrice { get; set; }

        public virtual Nomenclature Nomenclature { get; set; }
    }
}
