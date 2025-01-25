using Microsoft.AspNetCore.Mvc;
using NWTrackerAPI.Data;
using NWTrackerAPI.Models;
using NWTrackerAPI.Processors.Interfaces;

namespace NWTrackerAPI.Processors
{
    public class GetTrackerRecord : IGetTrackerRecord
    {
        public readonly APIContext context;

        public GetTrackerRecord(APIContext context)
        {
            this.context = context;
        }

        public TT_TRACKER get(APIContext context, int TrackerRecordPk)
        {
            var trackerRecord = context.TT_TRACKER.FirstOrDefault(tt => tt.TT_PK == TrackerRecordPk);

            if (trackerRecord != null)
            {
                return trackerRecord;
            }
            else
            {
                return null;
            }
        }
    }
}
