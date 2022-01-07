using Microsoft.AspNetCore.Mvc;
using QuizSystemWeb.Infrastructure;
using QuizSystemWeb.Services.Tests;

namespace QuizSystemWeb.Areas.Users.Controllers
{
    public class HomeController : UsersController
    {
        private readonly ITestService service;

        public HomeController(ITestService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            var model = service.GetAllActiveTests(this.User.Id());
            return View(model);
        }
    }
}
