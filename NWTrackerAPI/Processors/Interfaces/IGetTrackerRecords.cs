using NWTrackerAPI.Data;
using NWTrackerAPI.Models;

namespace NWTrackerAPI.Processors.Interfaces
{
    public interface IGetTrackerRecords
    {
        List<TrackerRecord> get(APIContext context, int ProjectPk);
    }
}