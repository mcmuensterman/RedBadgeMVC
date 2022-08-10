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
    public interface IMyEventService
    {
        bool CreateMyEvent(MyEventCreate model);
        IEnumerable<MyEventListItem> GetMyEvents();
        void SetUserId(Guid userId);
        MyEventDetail GetMyEventById(int id);
        bool UpdateMyEvent(MyEventEdit model);
        bool DeleteMyEvent(int id);
        IEnumerable<CategoryListItem> CreateCategoryList();
        IEnumerable<LocationListItem> CreateLocationList();

    }
}
