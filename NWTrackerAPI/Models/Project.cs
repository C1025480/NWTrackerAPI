using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace NWTrackerAPI.Models
{
    [ExcludeFromCodeCoverage]
    [Table("NW_PROJECTS")]
    public class Project
    {
        [Key]
        [Column("NW_PK")]
        public int NW_PK { get; set; }
        [Column("ProjectName")]
        public string ProjectName { get; set; }

    }
}
