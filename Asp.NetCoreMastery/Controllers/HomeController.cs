using Asp.NetCoreMastery.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Asp.NetCoreMastery.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IEmailSender emailSender, ISmsSender smsSender)
        {
            _logger = logger;
            _configuration = configuration;
            _emailSender = emailSender;
            _smsSender = smsSender;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("I am in index action");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Test()
        {
            var model = new TestModel();
            return View(model);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult Test(TestModel model)
        {
            if (ModelState.IsValid)
            {
                int x = 5;
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
