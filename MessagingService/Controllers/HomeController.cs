using Messaging.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MessagingService.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMessageRepository _MessageRepository;

        public HomeController(IMessageRepository MessageRepository)
        {
            _MessageRepository = MessageRepository;
        }

        public IActionResult Index()
        {
            var Message = 

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
