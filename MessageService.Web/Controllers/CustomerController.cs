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
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        ICustomerRepository _CustomerRepository;

        CustomerController(ICustomerRepository CustomerRepository)
        {
            _CustomerRepository = CustomerRepository;
        }

        [Authorize(Policy = "Customer")]
        // GET api/message/Customer
        [HttpGet]
        public ActionResult<IEnumerable<CustomerDTO>> GetStaff(int CustomerID)
        {
            return Ok(_CustomerRepository.GetCustomer(CustomerID));
        }

        // POST api/message
        [HttpPost]
        public IActionResult Post([FromBody] CustomerDTO Customer)
        {
            _CustomerRepository.InsertCustomer(Customer);
            return Ok();
        }

        [Authorize(Policy = "Staff")]
        // DELETE api/message?id=5
        [HttpDelete]
        public IActionResult Delete(int CustomerID)
        {
            _CustomerRepository.DeleteCustomer(CustomerID);
            return Ok();
        }


    }
}