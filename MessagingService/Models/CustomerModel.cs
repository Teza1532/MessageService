using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingService.Models
{
    public class CustomerModel
    {
        public int CustomerID { get; set; }
        public DateTime sent { get; set; }
        public string CustomerName { get; set; }

        public String SentFormatted()
        {
            var sent = this.sent;
            var now = DateTime.Now;
            double timeDiff = (now - sent).Minutes;

            if (timeDiff < 60)
            {
                return "Sent " + timeDiff.ToString() + " Minutes ago";
            }
            else
            {
                return "Sent " + (Math.Floor(timeDiff / 60)).ToString() + " hours and " + (timeDiff - (Math.Floor(timeDiff / 60))).ToString() + "minutes ago";
            }
        }

    }
}
