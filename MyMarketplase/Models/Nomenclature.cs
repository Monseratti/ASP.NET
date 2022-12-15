using System.Data.SqlTypes;

namespace MyMarketplase.Models
{
    public class Nomenclature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        
        public ICollection<FilesPath> Files { get; set; }
    }
}
