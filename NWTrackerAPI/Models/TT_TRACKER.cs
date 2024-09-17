using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace NWTrackerAPI.Models
{
    [ExcludeFromCodeCoverage]
    [Table("TT_TRACKER")]
    public class TT_TRACKER
    {
        [Key]
        public int TT_PK { get; set; }

        [ForeignKey("NW_Project")]
        public int TT_NW_FK { get; set; }

        public string TT_DPIRFC { get; set; }
        public string TT_UPRN { get; set; }
        public string TT_PRIORITY_ORDER { get; set; }

        [ForeignKey("SupportStatus")]
        public int? TT_STATUS { get; set; }

        public string TT_NOTES { get; set; }
        public string TT_HOUSE_NUMBER { get; set; }
        public string TT_STREET { get; set; }
        public string TT_COMMENTS { get; set; }

        public bool? TT_LINTEL_WORKS_REQUIRED { get; set; }

        [ForeignKey("SupportAccessEquipment")]
        public int? TT_ACCESS_EQUIPMENT_REQUIRED { get; set; }

        [ForeignKey("SupportConstructionType")]
        public int? TT_PROPERTY_CONSTRUCTION_TYPE { get; set; }

        public string TT_SURVEY_IDENTIFIED_COMMENTS { get; set; }
        public string TT_CALL_OFF_NUMBER { get; set; }
        public DateTime? TT_CALLED_OFF_DATE { get; set; }
        public DateTime? TT_DELIVERY_DATE { get; set; }

        [ForeignKey("SupportDeliveryPoint")]
        public int? TT_DELIVERY_POINT { get; set; }

        public bool? TT_DELIVERY_NOTE_RECEIVED { get; set; }
        public DateTime? TT_SCHEDULED_FITTING_DATE { get; set; }

        [ForeignKey("SupportTimeslot")]
        public int? TT_AMORPMAPPOINTMENT { get; set; }

        public string TT_ROUTE_MARCH_ORDER { get; set; }

        [ForeignKey("SupportAccessCall")]
        public int? TT_INSTALL_ACCESS_ATTEMPTS { get; set; }

        [ForeignKey("SupportInstaller")]
        public int? TT_PRIMARY_INSTALLER { get; set; }

        [ForeignKey("SupportInstaller")]
        public int? TT_SECONDARY_INSTALLER { get; set; }

        public string TT_INSTALLATION_RELATED_COMMENTS { get; set; }
        public bool? TT_INSTALLATION_SHEET_RECEIVED { get; set; }
        public string TT_VARIATION_DETAILS { get; set; }
        public string TT_VARIATION_ORDER_NUMBER { get; set; }
        public DateTime? TT_DATE_HANDED_OVER { get; set; }
        public string TT_INSPECTED_BY { get; set; }
        public string TT_INSTALL_WAGES_APPLIED_FOR { get; set; }

        public string TT_LEASE_HOLDER_HOUSE_NUMBER { get; set; }
        public string TT_LEASE_HOLDER_STREET { get; set; }
        public string LEASE_HOLDER_TOWN { get; set; }
        public string LEASE_HOLDER_COUNTRY { get; set; }
        public string LEASE_HOLDER_POSTCODE { get; set; }
        public string LEASE_HOLDER_NAME { get; set; }
        public string LEASE_HOLDER_PRIMARY_PHONE { get; set; }
        public string LEASE_HOLDER_SECONDARY_PHONE { get; set; }
        public string LEASE_HOLDER_EMAIL { get; set; }
    }
}
