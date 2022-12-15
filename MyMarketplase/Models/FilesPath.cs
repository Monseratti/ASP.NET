namespace MyMarketplase.Models
{
    public class FilesPath
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int NomID { get; set; }

        public virtual Nomenclature Nomenclature { get; set; }
    }
}
