using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace NWTrackerAPI.Models
{
    public class SS_SUPPORT_STATUS
    {
        [Key]
        public int SS_PK { get; set; }
        public string SS_Category_Name { get; set; }
    }
}
