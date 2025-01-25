using NWTrackerAPI.Data;
using NWTrackerAPI.Models;

namespace NWTrackerAPI.Processors.Interfaces
{
    public interface IGetTrackerRecord
    {
        TT_TRACKER get(APIContext context, int TrackerRecordPk);
    }
}