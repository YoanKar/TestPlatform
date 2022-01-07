namespace QuizSystemWeb.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using QuizSystemWeb.Areas.Admin.Models.Test;
    using QuizSystemWeb.Infrastructure;
    using QuizSystemWeb.Services.Tests;
    using QuizSystemWeb.Services.Tests.Models;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;

    public class TestsController : AdministratorController
    {
        private readonly ITestService service;

        public TestsController(ITestService service)
        {
            this.service = service;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TestFormModel model)
        {
            var authorId = this.User.Id();

            var succesfullyCreated = service.Create(model.Name, model.StartDate, model.EndDate, model.Duration, authorId);

            if (!succesfullyCreated)
            {
                this.TempData[WebConstants.GlobalErrorMessageKey] = "Can not create test because start date must be earlier than end date!";
            }
            else
            {
                this.TempData[WebConstants.GlobalMessageKey] = "Test was succesfully created!";
            }

            return RedirectToAction("All");
        }

        public IActionResult All()
        {
            var allTests = this.service.GetAllTests();

            return View(allTests);
        }

        public IActionResult Details(int id)
        {
            var test = this.service.Details(id);

            return View(test);

        }

        public IActionResult Edit(int id)
        {
            var test = this.service.Details(id);

            var testDto = new TestFormModel
            {
                Id = test.Id,
                Name = test.Name,
                StartDate = test.StartDate,
                EndDate = test.EndDate,
                Duration = test.Duration,
            };


            return View(testDto);
        }


        [HttpPost]
        public IActionResult Edit(TestFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!(service.Edit(model.Id, model.Name, model.StartDate, model.EndDate, model.Duration)))
            {
                this.TempData[WebConstants.GlobalErrorMessageKey] = "Can not edit test because start date must be earlier than end date!";
            }
            else
            {
                this.TempData[WebConstants.GlobalMessageKey] = "Test was succesfully edited!";
            }
            ;
            return RedirectToAction("Details", "Tests", new { id = model.Id });

        }

        public IActionResult ChangeVisibility(int id)
        {
            if (!this.service.ChangeVisibility(id))
            {
                this.TempData[WebConstants.GlobalErrorMessageKey] = "Test does not exists!";
            }
            else
            {
                this.TempData[WebConstants.GlobalMessageKey] = "Succesfully changed visibility of test!";
            }


            return RedirectToAction("All");

        }

        public IActionResult EvaluateTests()
        {
            var model = service.GetAllUnvaluatedTests();
            return View(model);
        }

        public IActionResult CheckOpenedQuestions(string userId, int testId, int resultId)
        {
            var model = service.GetOpenedAnswersForSolvedTest(userId, testId, resultId);
            return View(model);
        }

        [HttpPost]
        public async void CheckOpenedQuestions()
        {
            string body;
            using (var reader = new StreamReader(Request.Body))
            {
                body = await reader.ReadToEndAsync();
            }

            service.AddPointsToResult(body);
        }


        [HttpPost]
        public async void WriteGrade()
        {
            string body;
            using (var reader = new StreamReader(Request.Body))
            {
                body = await reader.ReadToEndAsync();
            }

            service.AddGradeToResult(body);
        }


        public IActionResult EvaluatedTests()
        {
            var evaluatedTests = this.service.GetAllEvaluatedTests();

            return View(evaluatedTests);
        }
    }
}
