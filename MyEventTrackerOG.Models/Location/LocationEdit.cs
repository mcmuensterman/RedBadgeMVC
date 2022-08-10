using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEventTrackerOG.Models.Location
{
    public class LocationEdit
    {
        [Required]
        public int LocationId { get; set; }
        [Required]
        public string LocationName { get; set; }
    }
}
