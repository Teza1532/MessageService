using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageService.Data.DTO;
using MessageService.Data.Repositories;
using MessagingService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace MessagingService.Controllers
{
    [Authorize]
    public class StaffMessageController : Controller
    {
        private readonly IMessageRepository _MessageRepository;
        private readonly ICustomerRepository _CustomerRepository;
        private readonly IStaffRepository _StaffRepository;

        public StaffMessageController(IMessageRepository MessageRepository, ICustomerRepository CustomerRepository, IStaffRepository StaffRepository)
        {
            _MessageRepository = MessageRepository;
            _CustomerRepository = CustomerRepository;
            _StaffRepository = StaffRepository;
        }

        [Authorize(Policy = "Staff")]
        [Route("Staff")]
        [Route("Staff/Index")]
        public IActionResult Index()
        {
            var staffClaims = User.Claims;
            int.TryParse(staffClaims.First(s => s.Type == "StaffID").Value, out int StaffID);

            List<MessageDTO> staffMessages =  _MessageRepository.StaffMessages(StaffID).ToList();

            ViewBag.Row = 1;

            ViewBag.Cust = staffMessages
                .OrderBy(s => s.Sent)                
                .Select(s => new CustomerModel()
                {
                   CustomerID = s.CustomerID,
                   sent = s.Sent,
                   CustomerName = s.CustomerName
                })
                .GroupBy(s => s.CustomerID)
                .First()
                .ToList();
                       
            return View();
        }
    }


}