namespace QuizSystemWeb.Areas.Users.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using QuizSystemWeb.Infrastructure;
    using QuizSystemWeb.Services.Questions;
    using QuizSystemWeb.Services.Tests;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public class TestsController : UsersController
    {
        private readonly IQuestionService questionService;
        private readonly ITestService testService;

        public TestsController(IQuestionService service, ITestService testService)
        {
            this.questionService = service;
            this.testService = testService;
        }

       
        public IActionResult Compete(int id)
        {
            var model = questionService.GetAllTestQuestions(id);

            return View(model);
        }

        [HttpPost]
        public async Task<int> Compete()
        {
            string body;
            using (var reader = new StreamReader(Request.Body))
            {
                 body = await reader.ReadToEndAsync();
            }
            
            int points = testService.SubmitUserAnswers(body, this.User.Id());

            return points;
        }

        public IActionResult Completed()
        {
            var completedTests = this.testService.CompletedTests(this.User.Id());

            return View(completedTests);

        }

        public IActionResult CheckTestAnswers(int testId)
        {
            var testQuestionsAnswers = this.testService.CheckTest(testId, this.User.Id());

            return View(testQuestionsAnswers);

        }
    }
}
