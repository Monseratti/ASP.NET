using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MyMarketplase.Models
{
    public class FilesPath
    {
        [ValidateNever]
        public int Id { get; set; }
        public string Path { get; set; }
        public int NomID { get; set; }
        [ValidateNever]
        public virtual Nomenclature Nomenclature { get; set; }
    }
}
