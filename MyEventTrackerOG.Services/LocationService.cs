using MyEventTrackerOG.Data;
using MyEventTrackerOG.Models.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEventTrackerOG.Services
{
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext _ctx;
        private Guid _userId;

        public LocationService(ApplicationDbContext context)
        {
            _ctx = context;
        }

        public bool CreateLocation(LocationCreate model)
        {
            var locationEntity = new Location()
            {
                OwnerId = _userId,
                Name = model.LocationName
            };

            _ctx.Locations.Add(locationEntity);
            return _ctx.SaveChanges() == 1;
        }

        public IEnumerable<LocationListItem> GetAllLocations()
        {
            var locations = _ctx.Locations
                .Where(e => e.OwnerId == _userId)
                .Select(e =>
                    new LocationListItem()
                    {
                        LocationId = e.Id,
                        LocationName = e.Name,
                        OwnerId = e.OwnerId
                    }).ToList();
            return locations;
        }

        public LocationDetail GetLocationById(int id)
        {
            var location = _ctx.Locations
                .Single(e => e.Id == id && e.OwnerId == _userId);
            return new LocationDetail()
            {
                LocationId = location.Id,
                LocationName = location.Name,
            };
        }

        public bool UpdateLocation(LocationEdit model)
        {
            var location = _ctx.Locations.Find(model.LocationId);
            if (location?.OwnerId != _userId || location is null) return false;
            location.Name = model.LocationName;


            return _ctx.SaveChanges() == 1;
        }

        public bool DeleteLocation(int locationId)
        {
            var entity = _ctx.Locations
                .SingleOrDefault(e => e.Id == locationId && e.OwnerId == _userId);
            _ctx.Locations.Remove(entity);

            return _ctx.SaveChanges() == 1;
        }


        public void SetUserId(Guid userId) => _userId = userId;
    }
}
