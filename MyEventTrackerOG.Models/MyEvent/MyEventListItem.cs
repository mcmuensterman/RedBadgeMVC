using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEventTrackerOG.Models.MyEvent
{
    public class MyEventListItem
    {
        public int MyEventId { get; set; }
        public string EventName { get; set; }

        public DateTime EventDate { get; set; }
    }
}
