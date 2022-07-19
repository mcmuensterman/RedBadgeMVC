using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEventTrackerOG.Models.MyEvent
{
    public class MyEventCreate
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Max 100 characters")]
        public string EventName { get; set; }
        public string Location { get; set; }
        [Required]
        [MaxLength(2000, ErrorMessage = "Max 2000 characters")]
        public string Content { get; set; }
        public DateTime EventDate { get; set; }
    }
}
