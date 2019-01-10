using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Data.DTO
{
    public class CustomerDTO
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Deleted { get; set; }
    }
}
