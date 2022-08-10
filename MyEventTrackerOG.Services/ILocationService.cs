using MyEventTrackerOG.Models.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEventTrackerOG.Services
{
    public interface ILocationService
    {
        bool CreateLocation(LocationCreate model);
        IEnumerable<LocationListItem> GetAllLocations();
        void SetUserId(Guid userId);
        LocationDetail GetLocationById(int id);
        bool UpdateLocation(LocationEdit model);
        bool DeleteLocation(int locationId);
    }
}