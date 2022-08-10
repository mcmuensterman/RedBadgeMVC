using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEventTrackerOG.Models.Category
{
    public class CategoryListItem
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
    }
}
