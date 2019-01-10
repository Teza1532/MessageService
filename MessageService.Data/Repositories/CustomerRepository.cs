using MessageService.Models.Models;
using MessagingService.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace MessageService.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private MessageServiceContext _context;
        private bool disposed = false;

        public CustomerRepository(MessageServiceContext context)
        {
            _context = context;
        }

        public Customer CustomerGetInfo(int CustomerID)
        {
            return _context.Customers.Find(CustomerID);
        }

        public void InsertCustomer(Customer Customer)
        {
            _context.Customers.Add(Customer);
        }

        public void UpdateCustomer(Customer Customer)
        {
            _context.Entry(Customer).State = EntityState.Modified;
        }

        public void DeleteCustomer(int CustomerID)
        {
            Customer customer = _context.Customers.Find(CustomerID);

            customer.Deleted = true;

            _context.Entry(customer).State = EntityState.Modified;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }


    public interface ICustomerRepository : IDisposable
    {
        void InsertCustomer(Customer customer);
        void DeleteCustomer(int customerID);
        void UpdateCustomer(Customer customer);
        void Save();
    }
}

