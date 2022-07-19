using MyEventTrackerOG.Data;
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
                    Content = model.Content,
                    //CategoryId = model.CategoryId
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
                        EventDate = e.EventDate
                    }
                    );
            return events.ToArray();

        }

        public MyEventDetail GetMyEventById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.MyEvents.Single(e => e.MyEventId == id && e.OwnerId == _userId);
                return new MyEventDetail
                {
                    MyEventId = entity.MyEventId,
                    EventName = entity.EventName,
                    Content = entity.Content,
                    //Category = entity.Category,
                    EventDate = entity.EventDate
                };
            }
        }

        public void SetUserId(Guid userId) => _userId = userId;

    }
}
