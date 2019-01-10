using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Models.Models
{
        public class Customer
        {
            public int CustomerID { get; set; }
            public string Name { get; set; }
            public DateTime LastUpdated { get; set; }
            public bool Deleted { get; set; }
            public virtual ICollection<Message> Messages { get; set; }

        }
}
