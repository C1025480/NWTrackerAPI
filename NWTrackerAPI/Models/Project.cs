using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace NWTrackerAPI.Models
{
    [ExcludeFromCodeCoverage]
    [Table("NW_PROJECTS")]
    public class Project
    {
        public int NW_PK { get; set; }
        public string ProjectName { get; set; }

    }
}
