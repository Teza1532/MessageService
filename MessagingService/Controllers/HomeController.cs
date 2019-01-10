using MessageService.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MessagingService.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMessageRepository _MessageRepository;
        private readonly ICustomerRepository _CustomerRepository;
        private readonly IStaffRepository _StaffRepository;


        public HomeController(IMessageRepository MessageRepository, ICustomerRepository CustomerRepository, IStaffRepository StaffRepository)
        {
            _MessageRepository = MessageRepository;
            _CustomerRepository = CustomerRepository;
            _StaffRepository = StaffRepository;
        }

        public IActionResult Index()
        {
            var customer = _CustomerRepository.;


            ViewData["Message"] = _MessageRepository.CustomerMessages(customerID);

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
