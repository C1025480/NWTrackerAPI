using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NWTrackerAPI.Data;
using NWTrackerAPI.Models;

namespace NWTrackerAPI.Controllers
{
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]/[action]")]
    public class TrackerController : ControllerBase
    {
        public readonly APIContext context;

        public TrackerController(APIContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("/GetTrackerRecords")]
        public IActionResult GetTrackerRecords(int ProjectPk)
        {
            var result = context.TT_TRACKER
        .Where(tt => tt.TT_NW_FK == ProjectPk)
        .Join(
            context.SS_SUPPORT_STATUS,
            tt => tt.TT_STATUS,
            ss => ss.SS_PK,
            (tt, ss) => new TrackerRecord
            {
                TT_PK = tt.TT_PK,
                TT_NW_FK = tt.TT_NW_FK,
                TT_UPRN = tt.TT_UPRN,
                TT_STATUS = ss.SS_Category_Name,
                TT_HOUSE_NUMBER = tt.TT_HOUSE_NUMBER,
                TT_STREET = tt.TT_STREET
            })
        .OrderByDescending(x => x.TT_PK)
        .ToList();

            return new JsonResult(Ok(result));
        }
        [HttpGet]
        [Route("/GetTrackerRecord")]
        public JsonResult GetTrackerRecord(int TrackerRecordPK)
        {
            var trackerRecord = context.TT_TRACKER.FirstOrDefault(tt => tt.TT_PK == TrackerRecordPK);

            if (trackerRecord != null)
            {
                return new JsonResult(trackerRecord);
            }
            else
            {
                return null;
            }
        }
        [HttpPost]
        [Route("/DeleteTrackerRecord")]
        public JsonResult DeleteTrackerRecord(int TrackerRecordPK)
        {
            var trackerRecord = context.TT_TRACKER.FirstOrDefault(tt => tt.TT_PK == TrackerRecordPK);

            if (trackerRecord == null)
            {
                return new JsonResult(new { success = false, message = "Tracker record not found." });
            }
            else
            {
                context.TT_TRACKER.Remove(trackerRecord);
                context.SaveChanges();

                return new JsonResult(new { success = true, message = $"Successfully removed tracker record with ID {TrackerRecordPK}." });
            }
        }

        [HttpPost]
        [Route("/UpdateTrackerRecord")]
        public IActionResult UpdateTrackerRecord([FromBody] TT_TRACKER UpdatedTrackerRecord)
        {
            var existingRecord = context.TT_TRACKER.FirstOrDefault(tt => tt.TT_PK == UpdatedTrackerRecord.TT_PK);

            if (existingRecord != null)
            {
                existingRecord.TT_DPIRFC = UpdatedTrackerRecord.TT_DPIRFC;
                existingRecord.TT_UPRN = UpdatedTrackerRecord.TT_UPRN;
                existingRecord.TT_STATUS = UpdatedTrackerRecord.TT_STATUS;
                existingRecord.TT_HOUSE_NUMBER = UpdatedTrackerRecord.TT_HOUSE_NUMBER;
                existingRecord.TT_STREET = UpdatedTrackerRecord.TT_STREET;
                existingRecord.TT_COMMENTS = UpdatedTrackerRecord.TT_COMMENTS;
                existingRecord.TT_LINTEL_WORKS_REQUIRED = UpdatedTrackerRecord.TT_LINTEL_WORKS_REQUIRED;
                existingRecord.TT_ACCESS_EQUIPMENT_REQUIRED = UpdatedTrackerRecord.TT_ACCESS_EQUIPMENT_REQUIRED;
                existingRecord.TT_PROPERTY_CONSTRUCTION_TYPE = UpdatedTrackerRecord.TT_PROPERTY_CONSTRUCTION_TYPE;
                existingRecord.TT_SURVEY_IDENTIFIED_COMMENTS = UpdatedTrackerRecord.TT_SURVEY_IDENTIFIED_COMMENTS;
                existingRecord.TT_CALL_OFF_NUMBER = UpdatedTrackerRecord.TT_CALL_OFF_NUMBER;
                existingRecord.TT_CALLED_OFF_DATE = UpdatedTrackerRecord.TT_CALLED_OFF_DATE;
                existingRecord.TT_DELIVERY_DATE = UpdatedTrackerRecord.TT_DELIVERY_DATE;
                existingRecord.TT_DELIVERY_POINT = UpdatedTrackerRecord.TT_DELIVERY_POINT;
                existingRecord.TT_DELIVERY_NOTE_RECEIVED = UpdatedTrackerRecord.TT_DELIVERY_NOTE_RECEIVED;
                existingRecord.TT_SCHEDULED_FITTING_DATE = UpdatedTrackerRecord.TT_SCHEDULED_FITTING_DATE;
                existingRecord.TT_AMORPMAPPOINTMENT = UpdatedTrackerRecord.TT_AMORPMAPPOINTMENT;
                existingRecord.TT_ROUTE_MARCH_ORDER = UpdatedTrackerRecord.TT_ROUTE_MARCH_ORDER;
                existingRecord.TT_INSTALL_ACCESS_ATTEMPTS = UpdatedTrackerRecord.TT_INSTALL_ACCESS_ATTEMPTS;
                existingRecord.TT_PRIMARY_INSTALLER = UpdatedTrackerRecord.TT_PRIMARY_INSTALLER;
                existingRecord.TT_SECONDARY_INSTALLER = UpdatedTrackerRecord.TT_SECONDARY_INSTALLER;
                existingRecord.TT_INSTALLATION_RELATED_COMMENTS = UpdatedTrackerRecord.TT_INSTALLATION_RELATED_COMMENTS;
                existingRecord.TT_INSTALLATION_SHEET_RECEIVED = UpdatedTrackerRecord.TT_INSTALLATION_SHEET_RECEIVED;
                existingRecord.TT_VARIATION_DETAILS = UpdatedTrackerRecord.TT_VARIATION_DETAILS;
                existingRecord.TT_VARIATION_ORDER_NUMBER = UpdatedTrackerRecord.TT_VARIATION_ORDER_NUMBER;
                existingRecord.TT_DATE_HANDED_OVER = UpdatedTrackerRecord.TT_DATE_HANDED_OVER;
                existingRecord.TT_INSPECTED_BY = UpdatedTrackerRecord.TT_INSPECTED_BY;
                existingRecord.TT_INSTALL_WAGES_APPLIED_FOR = UpdatedTrackerRecord.TT_INSTALL_WAGES_APPLIED_FOR;
                existingRecord.TT_LEASE_HOLDER_HOUSE_NUMBER = UpdatedTrackerRecord.TT_LEASE_HOLDER_HOUSE_NUMBER;
                existingRecord.TT_LEASE_HOLDER_STREET = UpdatedTrackerRecord.TT_LEASE_HOLDER_STREET;
                existingRecord.LEASE_HOLDER_TOWN = UpdatedTrackerRecord.LEASE_HOLDER_TOWN;
                existingRecord.LEASE_HOLDER_COUNTRY = UpdatedTrackerRecord.LEASE_HOLDER_COUNTRY;
                existingRecord.LEASE_HOLDER_POSTCODE = UpdatedTrackerRecord.LEASE_HOLDER_POSTCODE;
                existingRecord.LEASE_HOLDER_NAME = UpdatedTrackerRecord.LEASE_HOLDER_NAME;
                existingRecord.LEASE_HOLDER_PRIMARY_PHONE = UpdatedTrackerRecord.LEASE_HOLDER_PRIMARY_PHONE;
                existingRecord.LEASE_HOLDER_SECONDARY_PHONE = UpdatedTrackerRecord.LEASE_HOLDER_SECONDARY_PHONE;
                existingRecord.LEASE_HOLDER_EMAIL = UpdatedTrackerRecord.LEASE_HOLDER_EMAIL;

                context.SaveChanges();

                return Ok();
            }
            else
            {
                return NotFound($"Tracker record with TT_PK {UpdatedTrackerRecord.TT_PK} not found");
            }
            /*{
              "tT_PK": 2,
              "tT_NW_FK": 1002,
              "tT_DPIRFC": "string",
              "tT_UPRN": "string",
              "tT_PRIORITY_ORDER": "string",
              "tT_STATUS": 1,
              "tT_NOTES": "string",
              "tT_HOUSE_NUMBER": "string",
              "tT_STREET": "string",
              "tT_COMMENTS": "string",
              "tT_LINTEL_WORKS_REQUIRED": true,
              "tT_ACCESS_EQUIPMENT_REQUIRED": 1,
              "tT_PROPERTY_CONSTRUCTION_TYPE": 1,
              "tT_SURVEY_IDENTIFIED_COMMENTS": "string",
              "tT_CALL_OFF_NUMBER": "string",
              "tT_CALLED_OFF_DATE": "2024-12-30T13:04:08.043Z",
              "tT_DELIVERY_DATE": "2024-12-30T13:04:08.043Z",
              "tT_DELIVERY_POINT": 1,
              "tT_DELIVERY_NOTE_RECEIVED": true,
              "tT_SCHEDULED_FITTING_DATE": "2024-12-30T13:04:08.043Z",
              "tT_AMORPMAPPOINTMENT": 1,
              "tT_ROUTE_MARCH_ORDER": "string",
              "tT_INSTALL_ACCESS_ATTEMPTS": 1,
              "tT_PRIMARY_INSTALLER": 1,
              "tT_SECONDARY_INSTALLER": 2,
              "tT_INSTALLATION_RELATED_COMMENTS": "string",
              "tT_INSTALLATION_SHEET_RECEIVED": true,
              "tT_VARIATION_DETAILS": "string",
              "tT_VARIATION_ORDER_NUMBER": "string",
              "tT_DATE_HANDED_OVER": "2024-12-30T13:04:08.043Z",
              "tT_INSPECTED_BY": "string",
              "tT_INSTALL_WAGES_APPLIED_FOR": "string",
              "tT_LEASE_HOLDER_HOUSE_NUMBER": "string",
              "tT_LEASE_HOLDER_STREET": "string",
              "leasE_HOLDER_TOWN": "string",
              "leasE_HOLDER_COUNTRY": "string",
              "leasE_HOLDER_POSTCODE": "string",
              "leasE_HOLDER_NAME": "string",
              "leasE_HOLDER_PRIMARY_PHONE": "string",
              "leasE_HOLDER_SECONDARY_PHONE": "string",
              "leasE_HOLDER_EMAIL": "string"
               }*/
        }
    }
}
