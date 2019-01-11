using MessageService.Data.Context;
using MessageService.Data.DTO;
using MessageService.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace MessageService.Data.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private MessageServiceContext _context;
        private bool disposed = false;

        public StaffRepository(MessageServiceContext context)
        {
            _context = context;
        }

        public StaffDTO GetStaff(int StaffID)
        {
            return _context.Staffs.Select(c => new StaffDTO
            {
                StaffID = c.StaffID,
                StaffName = c.StaffName
            })
            .Where(c => c.StaffID == StaffID).First();
        }

        public void InsertStaff(StaffDTO Staff)
        {
            _context.Staffs.Add(new Staff
            {
                StaffID = Staff.StaffID,
                StaffName = Staff.StaffName,
                LastUpdated = DateTime.Now,
                Deleted = false
            });
        }

        public void UpdateStaff(StaffDTO Staff)
        {
            _context.Entry(new Staff
            {
                StaffID = Staff.StaffID,
                StaffName = Staff.StaffName

            }).State = EntityState.Modified;

            Save();
        }

        public void DeleteStaff(int StaffID)
        {
            Staff staff = _context.Staffs.Find(StaffID);

            staff.Deleted = true;
            staff.LastUpdated = DateTime.Now;
            _context.Entry(staff).State = EntityState.Modified;

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
    public interface IStaffRepository
    {
        StaffDTO GetStaff(int staffID);
        void InsertStaff(StaffDTO staff);
        void UpdateStaff(StaffDTO staff);
        void DeleteStaff(int staffID);
    }
}
