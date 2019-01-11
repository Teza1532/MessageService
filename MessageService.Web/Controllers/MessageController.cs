using MessageService.Data.DTO;
using MessageService.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MessageService.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        IMessageRepository _MessageRepository;

        MessageController(IMessageRepository MessageRepository)
        {
            _MessageRepository = MessageRepository;
        }

        [Authorize(Policy = "Customer")]
        // GET api/message/Customer
        [HttpGet("Customer")]
        public ActionResult<IEnumerable<MessageDTO>> GetCustomerMessages()
        {
            int.TryParse(User.Claims.First(u => u.Type == "UserId").Value, out int userID);
            return Ok(_MessageRepository.CustomerMessages(userID));
        }

        [Authorize(Policy = "Staff")]
        // Get api/message/Staff?StaffID=1
        [HttpGet("Staff")]
        public ActionResult<IEnumerable<MessageDTO>> GetStaffMessages()
        {
            int.TryParse(User.Claims.First(u => u.Type == "StaffId").Value, out int StaffID);
            return Ok(_MessageRepository.StaffMessages(StaffID));
        }

        // POST api/message
        [HttpPost]
        public void Post([FromBody] MessageDTO Message)
        {
            _MessageRepository.InsertMessage(Message);
        }
        
        // DELETE api/message?id=5
        [HttpDelete]
        public void Delete(int MessageID)
        {
            string sentByUserName;

            sentByUserName = User.Claims.First(u => u.Type == "StaffName").Value;

            if (String.IsNullOrEmpty(sentByUserName))
            {
                sentByUserName = User.Claims.First(u => u.Type == "CustomerName").Value;
            }

            _MessageRepository.DeleteMessage(MessageID, sentByUserName);
        }
    }
}