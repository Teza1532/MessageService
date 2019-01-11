using MessageService.Data.Context;
using MessageService.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MessageService.Data.Context
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


public static class DbInitializer
{
    public static void Initialize(MessageServiceContext MessageServiceContext)
    {
        MessageServiceContext.Database.EnsureCreated();
    }
}