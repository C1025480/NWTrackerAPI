using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NWTrackerAPI.Data;
using NWTrackerAPI.Models;
using NWTrackerAPI.Processors.Interfaces;

namespace NWTrackerAPI.Controllers
{
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]/[action]")]
    public class TrackerController : ControllerBase
    {
        public readonly APIContext context;
        IGetTrackerRecords getTrackerRecords;
        IGetTrackerRecord getTrackerRecord;
        IDeleteTrackerRecord deleteTrackerRecord;
        IUpdateTrackerRecord updateTrackerRecord;

        public TrackerController(APIContext context,
            IGetTrackerRecords getTrackerRecords,
            IGetTrackerRecord getTrackerRecord,
            IDeleteTrackerRecord deleteTrackerRecord,
            IUpdateTrackerRecord updateTrackerRecord)
        {
            this.context = context;
            this.getTrackerRecords = getTrackerRecords;
            this.getTrackerRecord = getTrackerRecord;
            this.deleteTrackerRecord = deleteTrackerRecord;
            this.updateTrackerRecord = updateTrackerRecord;
        }

        [HttpGet]
        [Route("/GetTrackerRecords")]
        public IActionResult GetTrackerRecords(int ProjectPk)
        {
            List<TrackerRecord> list = getTrackerRecords.get(context, ProjectPk);

            return new JsonResult(Ok(list));
        }
        [HttpGet]
        [Route("/GetTrackerRecord")]
        public JsonResult GetTrackerRecord(int TrackerRecordPk)
        {
            TT_TRACKER trackerRecord = this.getTrackerRecord.get(context, TrackerRecordPk);

            return new JsonResult(trackerRecord);
        }
        [HttpPost]
        [Route("/DeleteTrackerRecord")]
        public JsonResult DeleteTrackerRecord(int TrackerRecordPk)
        {
            bool trackerRecord = deleteTrackerRecord.delete(context, TrackerRecordPk); 

            if (trackerRecord == false)
            {
                return new JsonResult(new { success = false, message = "Tracker record not found." });
            }
            else
            {
                return new JsonResult(new { success = true, message = $"Successfully removed tracker record with ID {TrackerRecordPk}." });
            }
        }

        [HttpPost]
        [Route("/UpdateTrackerRecord")]
        public IActionResult UpdateTrackerRecord([FromBody] TT_TRACKER UpdatedTrackerRecord)
        {
            bool Success = updateTrackerRecord.update(context, UpdatedTrackerRecord);

            if (Success == false)
            {
                return NotFound($"Tracker record with TT_PK {UpdatedTrackerRecord.TT_PK} not found");
            }
            else
            {
                return Ok();
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
