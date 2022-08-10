using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEventTrackerOG.Data
{
    public class MyEvent
    {
        [Key]
        public int MyEventId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage ="Max 100 characters")]
        public string EventName { get; set; }

        [ForeignKey(nameof(Location))]
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        [Required]
        [MaxLength(2000, ErrorMessage = "Max 2000 characters")]
        public string Content { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }


    }
}
