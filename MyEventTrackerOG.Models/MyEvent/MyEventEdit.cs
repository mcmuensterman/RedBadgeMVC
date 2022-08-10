using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEventTrackerOG.Models.MyEvent
{
    public class MyEventEdit
    {
        [Required]
        public int MyEventId { get; set; }
        [Required]
        public string EventName { get; set; }
        [Required]
        public int LocationId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
        [Required]
        public int CategoryId { get; set; }

    }
}
