using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace NWTrackerAPI.Models
{
    [ExcludeFromCodeCoverage]
    [Table("LOG_LOGINS")]
    public class LOG_LOGIN
    {
        [Key]
        public int LOG_ID { get; set; }
        public string LOG_FIRSTNAME { get; set; }
        public string LOG_SECONDNAME { get; set; }
        public string LOG_EMAILADDRESS { get; set; }
        public string LOG_PHONENUMBER { get; set; }
        public string LOG_USERNAME { get; set; }
        public string? LOG_HASHED_PASSWORD { get; set; }
        public string? LOG_SALT { get; set; }

    }
}
