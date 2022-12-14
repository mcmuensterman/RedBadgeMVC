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
        [Display(Name = "ID")]
        public int MyEventId { get; set; }

        [Display(Name ="Name of Event")]
        public string EventName { get; set; }
        public int LocationId { get; set; }
        [Display(Name ="Event Location")]
        public string LocationName { get; set; }
        [Display(Name ="About")]
        public string Content { get; set; }

        [Display(Name="Event Date and Time")]
        public DateTime EventDate { get; set; }
        public int CategoryId { get; set; }
        [Display(Name ="Type of Event")]
        public string CategoryName { get; set; }
    }
}
