using MessageService.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingService.Data.Context
{
    public class MessageServiceContext : DbContext
    {
        public MessageServiceContext(DbContextOptions<MessageServiceContext> options) : base(options)
        {
        }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
