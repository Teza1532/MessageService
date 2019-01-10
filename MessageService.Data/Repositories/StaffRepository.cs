using MessageService.Models.Models;
using MessagingService.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace MessageService.Data.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private MessageServiceContext _context;
        private bool disposed = false;

        StaffRepository(MessageServiceContext context)
        {
            _context = context;
        }

        public void InsertStaff(Staff Staff)
        {
            _context.Staffs.Add(Staff);
        }

        public void UpdateStaff(Staff Staff)
        {
            _context.Entry(Staff).State = EntityState.Modified;
        }

        public void DeleteStaff(int StaffID)
        {
            Staff staff = _context.Staffs.Find(StaffID);

            staff.deleted = true;

            _context.Entry(staff).State = EntityState.Modified;
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
    public interface IStaffRepository
    {
        void InsertStaff(Staff Staff);
        void UpdateStaff(Staff Staff);
        void DeleteStaff(int StaffID);
    }
}
