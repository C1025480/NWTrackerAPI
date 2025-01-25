using Microsoft.AspNetCore.Mvc;
using NWTrackerAPI.Data;
using NWTrackerAPI.Models;
using NWTrackerAPI.Processors.Interfaces;

namespace NWTrackerAPI.Processors
{
    public class DeleteTrackerRecord : IDeleteTrackerRecord
    {
        public readonly APIContext context;

        public DeleteTrackerRecord(APIContext context)
        {
            this.context = context;
        }

        public bool delete(APIContext context, int TrackerRecordPk)
        {
            var trackerRecord = context.TT_TRACKER.FirstOrDefault(tt => tt.TT_PK == TrackerRecordPk);

            if (trackerRecord == null)
            {
                return false;
            }
            else
            {
                context.TT_TRACKER.Remove(trackerRecord);
                context.SaveChanges();

                return true;
            }
        }
    }
}
