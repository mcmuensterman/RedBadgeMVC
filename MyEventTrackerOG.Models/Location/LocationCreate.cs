using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEventTrackerOG.Models.Location
{
    public class LocationCreate
    {
        [Required]
        [MinLength(2, ErrorMessage = "Please enter a valid Location Name")]
        [MaxLength(100, ErrorMessage = "Too many characters. try again")]
        public string LocationName { get; set; }
    }
}
