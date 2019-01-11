using MessageService.Data.Context;
using MessageService.Data.DTO;
using MessageService.Models.Models;
using Microsoft.AspNetCore.Authorization;
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

        public IEnumerable<MessageDTO> CustomerMessages(int CustomerID)
        {
            return _context.Messages
                .Select(s => new MessageDTO
                {
                    MessageID = s.CustomerID,
                    message = s.message,
                    Sent = s.Sent,
                    SentbyUserName = s.SentbyUserName,
                    CustomerID = s.Customer.CustomerID,
                    StaffID = s.Staff.StaffID,
                    StaffName = s.Staff.StaffName,
                    CustomerName = s.Customer.CustomerName
                })
                .Where(x => x.CustomerID == CustomerID)
                .OrderBy(x => x.Sent.TimeOfDay)
                .ThenBy(x => x.Sent.Date)
                .ThenBy(x => x.Sent.Year);

        }

        public IEnumerable<MessageDTO> StaffMessages(int StaffID)
        {
            return _context.Messages
                .Select(s => new MessageDTO
                {
                    MessageID = s.MessageID,
                    message = s.message,
                    Sent = s.Sent,
                    SentbyUserName = s.SentbyUserName,
                    StaffName = s.Staff.StaffName,
                    CustomerName = s.Customer.CustomerName
                })            
                .Where(s => s.StaffID == StaffID 
                        && s.Deleted == false)
                .OrderBy(s => s.Sent.TimeOfDay)
                .ThenBy(s => s.Sent.Date)
                .ThenBy(s => s.Sent.Year);
        }

        public void InsertMessage(MessageDTO Message)
        {
            _context.Messages.Add(new Message
            {
                message = Message.message,
                Sent = DateTime.Now,
                SentbyUserName = Message.SentbyUserName,
                CustomerID = Message.CustomerID,
                StaffID = Message.StaffID
            });

            Save();
        }

        public void DeleteMessage(int MessageID, string SentbyUserName)
        {
            Message message = _context.Messages
                  .Where(m => m.MessageID == MessageID && m.SentbyUserName == SentbyUserName)
                  .First();
            
            message.LastUpdated = DateTime.Now;
            message.Deleted = true;

            _context.Entry(message).State = EntityState.Modified;

            Save();
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
        IEnumerable<MessageDTO> CustomerMessages(int CustomerID);
        IEnumerable<MessageDTO> StaffMessages(int StaffID);
        void InsertMessage(MessageDTO Message);
        void DeleteMessage(int MessageID, string sentByUserName);
        void Save();
    }
}
