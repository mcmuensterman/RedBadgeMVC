using Microsoft.EntityFrameworkCore;
using MyEventTrackerOG.Data;
using MyEventTrackerOG.Models.Category;
using MyEventTrackerOG.Models.Location;
using MyEventTrackerOG.Models.MyEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEventTrackerOG.Services
{
    public class MyEventService : IMyEventService
    {
        private readonly ApplicationDbContext _ctx;
        private Guid _userId;

        public MyEventService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public bool CreateMyEvent(MyEventCreate model)
        {
            var entity =
                new MyEvent
                {
                    OwnerId = _userId,
                    EventName = model.EventName,
                    LocationId = model.LocationId,
                    Content = model.Content,
                    CategoryId = model.CategoryId,
                    EventDate = model.EventDate
                };

            
                _ctx.MyEvents.Add(entity);
                return _ctx.SaveChanges() == 1;
            
            
        }

        public IEnumerable<MyEventListItem> GetMyEvents()
        {
            
                var events =
                    _ctx
                    .MyEvents.Where(e => e.OwnerId == _userId)
                    .Select(e => new MyEventListItem
                    {
                        MyEventId = e.MyEventId,
                        EventName = e.EventName,
                        EventDate = e.EventDate,
                        CategoryName = e.Category.Name
                    }
                    );
            return events.ToArray();

        }

        public MyEventDetail GetMyEventById(int id)
        {
            {
                var entity = _ctx.MyEvents.Include(e => e.Location)
                    .Include(e => e.Category).Single(e => e.MyEventId == id && e.OwnerId == _userId);
                return new MyEventDetail()
                {
                    MyEventId = entity.MyEventId,
                    EventName = entity.EventName,
                    LocationName = entity.Location.Name,
                    Content = entity.Content,
                    EventDate = entity.EventDate,
                    CategoryName=entity.Category.Name
                };
            }
        }

        public bool UpdateMyEvent(MyEventEdit model)
        {
            var myEvent = _ctx.MyEvents.Single(e => e.MyEventId == model.MyEventId && e.OwnerId == _userId);

            myEvent.EventName = model.EventName;
            myEvent.LocationId = model.LocationId;
            myEvent.Content = model.Content;
            myEvent.EventDate = model.EventDate;


            return _ctx.SaveChanges() == 1;
        }

        public bool DeleteMyEvent(int myEventId)
        {
            var entity = _ctx.MyEvents
                .SingleOrDefault(e => e.MyEventId == myEventId && e.OwnerId == _userId);

            _ctx.MyEvents.Remove(entity);

            return _ctx.SaveChanges() == 1;
        }

        public IEnumerable<CategoryListItem> CreateCategoryList()
        {
            var categoryService = new CategoryService(_ctx);
            categoryService.SetUserId(_userId);
            var userCategories = categoryService.GetAllCategories().Where(e => e.OwnerId == _userId);
            return userCategories;
        }

        public IEnumerable<LocationListItem> CreateLocationList()
        {
            var locationService = new LocationService(_ctx);
            locationService.SetUserId(_userId);
            var userLocations = locationService.GetAllLocations().Where(e => e.OwnerId == _userId);
            //if (userLocations == null)
            //{
            //    var defaultLocation = new LocationListItem()
            //    {
            //        LocationId = 0,
            //        LocationName = "Location",
            //        OwnerId = _userId

            //    };
            //    userLocations.Append(defaultLocation);
            //}
            return userLocations;
        }

        public void SetUserId(Guid userId) => _userId = userId;

    }
}
