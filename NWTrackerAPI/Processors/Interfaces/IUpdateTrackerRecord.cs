using NWTrackerAPI.Data;
using NWTrackerAPI.Models;

namespace NWTrackerAPI.Processors.Interfaces
{
    public interface IUpdateTrackerRecord
    {
        bool update(APIContext context, TT_TRACKER TrackerRecord);
    }
}