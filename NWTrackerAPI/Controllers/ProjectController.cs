using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NWTrackerAPI.Data;
using NWTrackerAPI.Models;

namespace NWTrackerAPI.Controllers
{
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]/[action]")]
    public class ProjectController : ControllerBase
    {
        public readonly APIContext context;

        public ProjectController(APIContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("/GetProjects")]
        public JsonResult GetProjects()
        {
           var result = context.Projects.ToList();

            return new JsonResult(Ok(result));
        }
        [HttpPost]
        [Route("/NewProject")]
        public IActionResult NewProject([FromBody] Project inputtedProjectName)
        {
            bool AlreadyExists = context.Projects.Any(x => x.ProjectName == inputtedProjectName.ProjectName);

            if (AlreadyExists == true)
            {
                return BadRequest("Project name already Exists");
            }

            if (inputtedProjectName == null || string.IsNullOrWhiteSpace(inputtedProjectName.ProjectName))
            {
                return BadRequest("Project name is required.");
            }

            var new_project = new Project()
            {
                ProjectName = inputtedProjectName.ProjectName
            };

            context.Projects.Add(new_project);
            context.SaveChanges();

            return Ok();
        }
        [HttpGet]
        [Route("/GetTrackerRecords")]
        public IActionResult GetTrackerRecords(int ProjectPk)
        {
            var result = context.TT_TRACKER
                .Where(tt => tt.TT_NW_FK == ProjectPk)
                .Select(tt => new TrackerRecord
                {
                TT_PK = tt.TT_PK,
                TT_NW_FK = tt.TT_NW_FK, 
                TT_UPRN = tt.TT_UPRN,
                TT_STATUS = tt.TT_STATUS,
                TT_HOUSE_NUMBER = tt.TT_HOUSE_NUMBER,
                TT_STREET = tt.TT_STREET,
                })
            .ToList();

            return new JsonResult(Ok(result));
        }
        [HttpGet]
        [Route("/GetTrackerRecord")]
        public JsonResult GetTrackerRecord(int TrackerRecordPK)
        {
            var result = context.TT_TRACKER
                .Where(tt => tt.TT_PK ==TrackerRecordPK)
                .ToList();

            return new JsonResult(Ok(result));
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

                return Ok("Tracker record updated successfully.");
            }
            else
            {
                return NotFound($"Tracker record with TT_PK {UpdatedTrackerRecord.TT_PK} not found");
            }
        }
    }
}
