using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMarketplase.Models
{
    public class FilesPath
    {
        [ValidateNever]
        public int Id { get; set; }
        public string Path { get; set; }
        [ForeignKey("Nomenclature")]
        public int NomID { get; set; }
        [ValidateNever]
        public virtual Nomenclature Nomenclature { get; set; }
    }
}
