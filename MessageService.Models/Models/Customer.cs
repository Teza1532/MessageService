using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MessageService.Models.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<Message> Messages { get; set; }

    }
}
