using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEventTrackerOG.Models.MyEvent
{
    public class MyEventDetail
    {
        public int MyEventId { get; set; }
        public string EventName { get; set; }
        public string Location { get; set; }
        public string Content { get; set; }
        [Display(Name="Event Date and Time")]
        public DateTime EventDate { get; set; }
    }
}
