using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NWTrackerAPI.Data;
using NWTrackerAPI.Models;
using NWTrackerAPI.Processors.Interfaces;

namespace NWTrackerAPI.Processors
{
    public class GetTrackerRecords : IGetTrackerRecords
    {
        public readonly APIContext context;

        public GetTrackerRecords(APIContext context)
        {
            this.context = context;
        }

        public List<TrackerRecord> get(APIContext context, int ProjectPk)
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

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
