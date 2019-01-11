using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Models.Models
{
    public class Staff
    {
        public int StaffID { get; set; }
        public string StaffName { get; set; }
        public DateTime LastUpdated { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public bool Deleted { get; set; }
    }
}
