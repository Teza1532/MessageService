using MessageService.Models.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessageService.Models.Models
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageID { get; set; }
        public string message { get; set; }
        public DateTime Sent { get; set; }
        public bool Deleted { get; set; }
        public int SentbyID { get; set; }
        public DateTime LastUpdated { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Staff Staff { get; set; }

        public int CustomerID { get; set; }
        public int StaffID { get; set; }
    }
}
