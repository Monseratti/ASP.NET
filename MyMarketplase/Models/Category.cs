namespace MyMarketplase.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Nomenclature> Nomenclatures { get; set; }
    }
}
