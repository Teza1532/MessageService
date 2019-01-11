using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageService.Data.DTO;
using MessageService.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessageService.Web.Controllers
{   [Authorize]
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
            int sentByID = 0;

            int.TryParse(User.Claims.First(u => u.Type == "StaffId").Value, out sentByID);

            if (sentByID == 0)
            {
                int.TryParse(User.Claims.First(u => u.Type == "CustomerId").Value, out sentByID);
            }

            _MessageRepository.DeleteMessage(MessageID, sentByID);
        }
    }
}