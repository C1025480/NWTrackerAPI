using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace NWTrackerAPI.Models
{
    public class TrackerRecord
    {
        public int TT_PK { get; set; }
        [ForeignKey("NW_Project")]
        public int TT_NW_FK { get; set; }
        public string TT_UPRN { get; set; }
        [ForeignKey("SupportStatus")]
        public int? TT_STATUS { get; set; }
        public string TT_HOUSE_NUMBER { get; set; }
        public string TT_STREET { get; set; }
    }
}
