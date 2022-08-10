using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEventTrackerOG.Models.Location
{
    public class LocationListItem
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public Guid OwnerId { get; set; }

    }
}
