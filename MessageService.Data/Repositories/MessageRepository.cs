using MessageService.Models.Models;
using MessagingService.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MessageService.Data.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private MessageServiceContext _context;
        private bool disposed = false;

        public MessageRepository(MessageServiceContext context)
        {
            _context = context;
        }

        public IEnumerable<Message> CustomerMessages(int CustomerID)
        {
            return _context.Messages.Where(x => x.CustomerID == CustomerID)
                                    .OrderBy(x => x.sent.TimeOfDay)
                                    .ThenBy(x => x.sent.Date)
                                    .ThenBy(x => x.sent.Year);
        }

        public IEnumerable<Message> StaffMessages(int StaffID)
        {
            return _context.Messages.Where(x => x.StaffID == StaffID)
                                    .OrderBy(x => x.sent.TimeOfDay)
                                    .ThenBy(x => x.sent.Date)
                                    .ThenBy(x => x.sent.Year);
        }

        public void InsertMessage(Message Message)
        {
            _context.Messages.Add(Message);
        }

        public void DeleteMessage(int MessageID)
        {
            Message message = _context.Messages.Find(MessageID);

            message.deleted = true;

            _context.Entry(message).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool Disposing)
        {
            if (!this.disposed)
            {
                if (Disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public interface IMessageRepository
    {
        IEnumerable<Message> CustomerMessages(int CustomerID);
        IEnumerable<Message> StaffMessages(int StaffID);
        void InsertMessage(Message Message);
        void DeleteMessage(int MessageID);
        void Save();
    }


}
