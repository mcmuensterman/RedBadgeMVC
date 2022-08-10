using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEventTrackerOG.Models.MyEvent
{
    public class MyEventListItem
    {
        [Display(Name = "Event Id")]
        public int MyEventId { get; set; }

        [Display(Name = "Event Name")]
        public string EventName { get; set; }

        public DateTime EventDate { get; set; }
        public string CategoryName { get; set; }
    }
}
