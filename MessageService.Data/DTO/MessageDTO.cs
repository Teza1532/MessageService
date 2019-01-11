using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Data.DTO
{
    public class MessageDTO
    {
        public int MessageID { get; set; }
        public string message { get; set; }
        public DateTime Sent { get; set; }
        public int SentbyID { get; set; }
        public int CustomerID { get; set; }
        public int StaffID { get; set; }
        public bool Deleted { get; set; }
    }
}
