using NWTrackerAPI.Data;

namespace NWTrackerAPI.Processors.Interfaces
{
    public interface IDeleteTrackerRecord
    {
        bool delete(APIContext context, int TrackerRecordPk);
    }
}