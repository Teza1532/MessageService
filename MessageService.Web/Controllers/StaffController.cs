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
    public class StaffController : ControllerBase
    {
        IStaffRepository _StaffRepository;

        StaffController(IStaffRepository StaffRepository)
        {
            _StaffRepository = StaffRepository;
        }

        [Authorize(Policy = "Staff")]
        // GET api/message/Customer
        [HttpGet]
        public ActionResult<IEnumerable<StaffDTO>> GetStaff(int StaffID)
        {
            return Ok(_StaffRepository.GetStaff(StaffID));
        }

        // POST api/message
        [HttpPost]
        public IActionResult Post([FromBody] StaffDTO Staff)
        {
            _StaffRepository.InsertStaff(Staff);
            return Ok();
        }

        [Authorize(Policy = "Staff")]
        // DELETE api/message?id=5
        [HttpDelete]
        public IActionResult Delete(int StaffID)
        {
            _StaffRepository.DeleteStaff(StaffID);
            return Ok();
        }
    }
}
