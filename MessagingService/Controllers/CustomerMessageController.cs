using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MessagingService.Controllers
{
    [Authorize]
    public class CustomerMessageController : Controller
    {
        [Authorize(Policy = "Staff")]
        public IActionResult StaffCustomerView()
        {
            var claims = User.Claims;
            int.TryParse(claims.First(c => c.Type == "UserID").Value, out int UserId);




            return View();
        }
    }
}