using MessageService.Data.Context;
using MessageService.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using MessageService.Data.DTO;
using System.Linq;

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

        public CustomerDTO GetCustomer(int CustomerID)
        {
            return _context.Customers.Select(c => new CustomerDTO
            {
                CustomerID = c.CustomerID,
                CustomerName = c.CustomerName
            })
            .Where(c => c.CustomerID == CustomerID).First();
        }

        public void InsertCustomer(CustomerDTO Customer)
        {
            _context.Customers.Add(new Customer
            {
                CustomerID = Customer.CustomerID,
                CustomerName = Customer.CustomerName,
                LastUpdated = DateTime.Now,
                Deleted = false
            });

            Save();
        }

        public void UpdateCustomer(CustomerDTO Customer)
        {
            _context.Entry(new Customer
            {
                CustomerID = Customer.CustomerID,
                CustomerName = Customer.CustomerName           
            }).State = EntityState.Modified;

            Save();
        }

        public void DeleteCustomer(int CustomerID)
        {
            Customer customer = _context.Customers.Find(CustomerID);

            customer.Deleted = true;
            customer.LastUpdated = DateTime.Now;

            _context.Entry(customer).State = EntityState.Modified;

            Save();
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
        CustomerDTO GetCustomer(int customerID);
        void InsertCustomer(CustomerDTO customer);
        void DeleteCustomer(int customerID);
        void UpdateCustomer(CustomerDTO customer);
        void Save();
    }
}

